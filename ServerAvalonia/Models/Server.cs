using ServerAvalonia.Data;
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

namespace ServerAvalonia.Models
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
        static int _countRequests = 0;
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly EndPoint? _remoteEndPoint;
        private readonly Task _readingTask;
        private readonly Task _writingTask;
        private readonly Action<Connection> _disposeCallback;
        private readonly Channel<string> _channel;
        bool disposed;

        public Connection(TcpClient client, Action<Connection> disposeCallback)
        {
            _client = client;
            _stream = client.GetStream();
            _remoteEndPoint = client.Client.RemoteEndPoint;
            _disposeCallback = disposeCallback;
            _channel = Channel.CreateUnbounded<string>();
            _readingTask = RunReadingLoop();
            _writingTask = RunWritingLoop();
        }

        //цикл  чтения получаемых сообщений + отправка ответных сообщений
        private async Task RunReadingLoop()
        {
            //тут, как я понял, принуждаем машину вернуть таск,
            //что фиксит какую-то ошибку, связанную с работой сокетов в дотнете
            await Task.Yield(); // https://ru.stackoverflow.com/a/1422205/373567
            try
            {
                //буфер для заголовка
                byte[] headerBuffer = new byte[4];
                while (true)
                {
                    int bytesReceived = await _stream.ReadAsync(headerBuffer, 0, 4);
                    if (bytesReceived != 4)
                        break;
                    //длина принимаемого сообщения
                    int length = BinaryPrimitives.ReadInt32LittleEndian(headerBuffer);
                    //буффер для сообщения
                    byte[] buffer = new byte[length];
                    //количество пропускаемых байт 
                    int count = 0;
                    //чтение сообщения
                    while (count < length)
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
                    Temp.MainViewModel.Answer += 
                        $"Date: {DateTime.Now} " +      //дата
                        Environment.NewLine +      
                        $"Point: {_remoteEndPoint}" +   //ip/port
                        Environment.NewLine + 
                        message + Environment.NewLine +  "_countReauests: " + _countRequests  + Environment.NewLine;  //само сообщение
                    _countRequests++;
                    Debug.WriteLine("_countReauests: " + _countRequests);


                    //ответное сообщение клиенту, пока что просто эхо
                    await SendMessageAsync($"Echo: {message}");
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




        //отправить сообщение
        public async Task SendMessageAsync(string message)
        {
            message = "request from server :  " + message + Environment.NewLine;
            await _channel.Writer.WriteAsync(message);
        }
        //цикл записи
        private async Task RunWritingLoop()
        {
            //в заголовке будет храниться длина сообщения
            byte[] header = new byte[4];

            await foreach (string message in _channel.Reader.ReadAllAsync())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                BinaryPrimitives.WriteInt32LittleEndian(header, buffer.Length);
                await _stream.WriteAsync(header, 0, header.Length);
                await _stream.WriteAsync(buffer, 0, buffer.Length);
            }
        }

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
                _channel.Writer.Complete();
                _stream.Close();
                //ожидаем завершение задач чтения/записи
                Task.WaitAll(_readingTask, _writingTask);
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

