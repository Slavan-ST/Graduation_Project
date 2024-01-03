
using ServerAvalonia.Data;
using ServerAvalonia.Models;
using ServerAvalonia.Services;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ServerAvalonia
{
    class TcpServer : IDisposable
    {
        private readonly TcpListener _listener;

        // это пул подключений,
        // нужен чтобы нормально отключить всех подключенных
        // при остановке сервера
        private readonly List<Connection> _clients;


        public TcpServer(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _clients = new List<Connection>();
        }

        public async Task ListenAsync()
        {
            try
            {
                _listener.Start();
                while (true)
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    //добавляем в пулл новое подключение
                    lock (_clients)
                    {
                        _clients.Add(
                            new Connection(client, c =>
                            {
                                //при закрытии подключения,
                                //не забываем убрать подключение из пула
                                lock (_clients)
                                {
                                    _clients.Remove(c);
                                }
                                c.Dispose();
                            }));
                    }
                }
            }
            catch (SocketException)
            {
                Debug.WriteLine("Сервер остановлен.");
            }
        }
        //остановка сервера
        public void Stop()
        {
            _listener.Stop();
        }

        #region Dispose
        bool disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                throw new ObjectDisposedException(typeof(TcpServer).FullName);
            disposed = true;
            _listener.Stop();
            if (disposing)
            {
                lock (_clients)
                {
                    if (_clients.Count > 0)
                    {
                        Console.WriteLine("Отключаю клиентов...");
                        foreach (Connection client in _clients)
                        {
                            client.Dispose();
                        }
                        Console.WriteLine("Клиенты отключены.");
                    }
                }
            }
        }

        ~TcpServer() => Dispose(false);
        #endregion
    }
    class Connection : IDisposable
    {
        #region поля и конструктор
        static int _countRequests = 0;
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly EndPoint? _remoteEndPoint;
        private readonly Task _readingTask;
        private readonly Task _writingQueryTask;
        private readonly Action<Connection> _disposeCallback;
        private readonly Channel<Answer> _channelForQuery;
        bool disposed;

        public Connection(TcpClient client, Action<Connection> disposeCallback)
        {
            _client = client;
            _stream = client.GetStream();
            _remoteEndPoint = client.Client.RemoteEndPoint;
            _disposeCallback = disposeCallback;

            _channelForQuery = Channel.CreateUnbounded<Answer>();
            _readingTask = RunReadingLoop();
            _writingQueryTask = RunWritingQueryLoop();
        }
        #endregion

        #region чтение получаемых сообщений
        //цикл  чтения получаемых сообщений + отправка ответных сообщений
        private async Task RunReadingLoop()
        {
            //тут, как я понял, принуждаем машину вернуть таск,
            //что фиксит какую-то ошибку, связанную с работой сокетов в дотнете
            await Task.Yield(); // https://ru.stackoverflow.com/a/1422205/373567
            try
            {
                //буфер для заголовка
                //вообще это зарезервиванное место под длину сообщения и что-то такое есть в TCP протоколе
                byte[] lengthMessageBytes = new byte[4];
                while (true)
                {
                    //получаем длину сообщения
                    int bytesReceived = await _stream.ReadAsync(lengthMessageBytes, 0, 4);


                    //если заголовок не равен 4 байтам, то прерываем цикл
                    if (bytesReceived != 4)
                        break;
                    //длина принимаемого сообщения, пока что только длина
                    int lengthMessage = BinaryPrimitives.ReadInt32LittleEndian(lengthMessageBytes);


                    //длина заголовка запроса
                    byte[] headerQueryLength = new byte[4];
                    bytesReceived = await _stream.ReadAsync(headerQueryLength, 0, headerQueryLength.Length);
                    if (bytesReceived != 4)
                        break;
                    //получаем размер сообщения
                    int lengthHeader = BinaryPrimitives.ReadInt32LittleEndian(headerQueryLength);
                    //прочитать из полученного сообщения заголовок
                    byte[] headerQueryBytes = new byte[lengthHeader];
                    bytesReceived = await _stream.ReadAsync(headerQueryBytes, 0, headerQueryBytes.Length);
                    //перевести его в текст
                    string header = Encoding.UTF8.GetString(headerQueryBytes);



                    //количество пропускаемых байт 
                    int count = 0;
                    //буффер для сообщения
                    byte[] buffer = new byte[lengthMessage];
                    //чтение сообщения
                    while (count < lengthMessage)
                    {
                        //тут мы считываем сообщение полученное от клиента
                        //и потом добавляем в
                        //"количество пропускаемых байт" длину принимаемого сообщения
                        //так, если клиент отправит ещё одно сообщение, 
                        //то считывание начнётся именно с этого сообщения, а не с начала
                        // или...
                        //надо тестить......
                        bytesReceived = await _stream.ReadAsync(buffer, count, buffer.Length - count);
                        count += bytesReceived;
                    }


                    //преобразуем полученное сообщение в текст
                    string message = Encoding.UTF8.GetString(buffer);
                    Debug.WriteLine($"Server!!<< {_remoteEndPoint}: {message}");

                    //сообщение, полученное от клиента
                    //это просто вывод в логи
                    Temp.MainViewModel.Answer +=
                        $"Date: {DateTime.Now} " +      //дата
                        Environment.NewLine +
                        $"Point: {_remoteEndPoint}" +   //ip/port
                        Environment.NewLine +
                        message + Environment.NewLine + "_countReauests: " + _countRequests + Environment.NewLine;  //само сообщение
                    _countRequests++;
                    Debug.WriteLine("_countReauests: " + _countRequests);




                    //ответное сообщение клиенту
                    string answer = DataBase.Select(header);
                    byte[] answerByte = Encoding.UTF8.GetBytes(answer);
                    await SendMessageAsync(new Answer("text", answerByte));
                }
                Console.WriteLine($"Клиент {_remoteEndPoint} отключился.");
                _stream.Close();
            }
            catch (IOException)
            {
                Debug.WriteLine($"Подключение к {_remoteEndPoint} закрыто сервером.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }
            if (!disposed)
                _disposeCallback(this);
        }
        #endregion


        #region запись и отправка сообщений
        //отправить запрос
        public async Task SendMessageAsync(Answer query)
        {
            await _channelForQuery.Writer.WriteAsync(query);
        }
        //цикл записи query
        private async Task RunWritingQueryLoop()
        {
            //заголовок - длина содержимого
            byte[] lengthContent = new byte[4];

            //заголовок запроса
            byte[] headerQueryLengthBytes = new byte[4];

            //тут изменить заголовок
            await foreach (Answer query in _channelForQuery.Reader.ReadAllAsync())
            {
                byte[] headerQueryBytes = Encoding.UTF8.GetBytes(query.Header);
                //буфер сообщения + его длина
                byte[] buffer = query.Content;
                BinaryPrimitives.WriteInt32LittleEndian(lengthContent, buffer.Length);  //длина сообщения
                await _stream.WriteAsync(lengthContent, 0, lengthContent.Length);       //длина сообщения

                //записываем длину заголовка, а также сам заголовок
                BinaryPrimitives.WriteInt32LittleEndian(headerQueryLengthBytes, headerQueryBytes.Length);
                await _stream.WriteAsync(headerQueryLengthBytes, 0, headerQueryLengthBytes.Length); //длина заголовка
                await _stream.WriteAsync(headerQueryBytes, 0, headerQueryBytes.Length); //пишем зоголовок

                //записываем само содержимое
                await _stream.WriteAsync(buffer, 0, buffer.Length);//содержимое сообщения
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().FullName);
            disposed = true;
            if (_client.Connected)
            {
                _channelForQuery.Writer.Complete();
                _stream.Close();
                //ожидаем завершение задач чтения/записи
                Task.WaitAll(_readingTask,_writingQueryTask);
            }
            if (disposing)
            {
                _client.Dispose();
            }
        }

        ~Connection() => Dispose(false);
        #endregion
    }


}

