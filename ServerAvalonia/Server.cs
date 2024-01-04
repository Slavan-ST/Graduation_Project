
using Helper.Models;
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
                    int bytesReceived = 0;


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
                    string headerText = Encoding.UTF8.GetString(headerQueryBytes);
                    HeaderClient header = new HeaderClient(headerText); //парсим
                    Temp.MainViewModel.Answer +=
                        $"Type: {header.TypeQuery}" + Environment.NewLine + 
                        $"Query: {header.QueryText}";

                    //количество пропускаемых байт 
                    int count = 0;
                    //буффер для сообщения
                    byte[] buffer = new byte[lengthMessage];
                    //чтение сообщения
                    while (count < lengthMessage)
                    {
                        bytesReceived = await _stream.ReadAsync(buffer, count, buffer.Length - count);
                        count += bytesReceived;
                    }

                    SendAnswer(header, buffer);
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
        private async void SendAnswer(HeaderClient header, byte[]? buffer = null)
        {

            List< ParametrQuery> parametrs = new List<ParametrQuery>();
            foreach (var p in header.ParamsQuery)
            {
                parametrs.Add(new ParametrQuery(p, buffer!));
            }

            string content = "";
            string status = "OK";
            string contentType = "text";           //потом поменять
            if (header.TypeQuery == "SELECT")
            {
                content = DataBase.Select(header.QueryText);
            }
            if (header.TypeQuery == "UPDATE")
            {
                content = DataBase.Update(header.QueryText, parametrs);
            }
            if (header.TypeQuery == "DELETE")
            {
                content = DataBase.Delete(header.QueryText);
            }
            if (header.TypeQuery == "CREATE")
            {
                content = DataBase.Create(header.QueryText, parametrs);
            }

            byte[] contentByte = Encoding.UTF8.GetBytes(content);
            Answer answer = new Answer(new HeaderServer(status, contentType), contentByte);
            //ответное сообщение клиенту
            Debug.WriteLine(content);
            await SendMessageAsync(answer);
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
                byte[] headerQueryBytes = Encoding.UTF8.GetBytes(query.Header.GetText());
                //буфер сообщения + его длина
                byte[] buffer = query.Content ?? Encoding.UTF8.GetBytes("ERROR"); 
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

