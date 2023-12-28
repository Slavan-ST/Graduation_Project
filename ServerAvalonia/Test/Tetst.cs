using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ServerAvalonia.Test.Server
{
    class ProgramServer
    {
        public static async Task MainServer(string[] args)
        {
            int port = 13400;
            Console.WriteLine("Запуск сервера....");
            //Запуск сервера
            using (TcpServer server = new TcpServer(port))
            {
                Task serverTask = server.ListenAsync();
                while (true)
                {
                    string input = Console.ReadLine();
                    // условие для остановки сервера
                    if (input == "stop")
                    {
                        Console.WriteLine("Остановка сервера...");
                        server.Stop();
                        break;
                    }
                }
                await serverTask;
            }
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey(true);
        }
    }

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
                Console.WriteLine("Сервер стартовал на " + _listener.LocalEndpoint);
                while (true)
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    Console.WriteLine(
                        $"Подключение: {client.Client.RemoteEndPoint} > " +
                        client.Client.LocalEndPoint);
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
                Console.WriteLine("Сервер остановлен.");
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

        //цикл  чтения получаемых сообщений
        private async Task RunReadingLoop()
        {
            //тут, как я понял, принуждаем машину вернуть таск
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
                    Console.WriteLine($"<< {_remoteEndPoint}: {message}");
                    await SendMessageAsync($"Echo: {message}");
                }
                Console.WriteLine($"Клиент {_remoteEndPoint} отключился.");
                _stream.Close();
            }
            catch (IOException)
            {
                Console.WriteLine($"Подключение к {_remoteEndPoint} закрыто сервером.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }
            if (!disposed)
                _disposeCallback(this);
        }
        //отправить сообщение
        public async Task SendMessageAsync(string message)
        {
            message = "request from server :  " + message;

            Console.WriteLine($">> {_remoteEndPoint}: ответ сервера: {message}");
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
namespace ServerAvalonia.Test.Client
{
    class ProgramClient
    {
        public static async Task MainClient(string[] args)
        {
            //ну порт
            int port = 13400;
            Console.WriteLine("Запуск клиента....");
            try
            {
                //создаём клиента
                using TcpClient tcpClient = new TcpClient("127.0.0.1", port);
                using Connection connection = new Connection(tcpClient);

                Console.WriteLine($"Подключен к серверу: {port}");
                while (true)
                {
                    string? input = Console.ReadLine();
                    if (input!.Length == 0)
                        break;
                    //отправляем серверу сообщение 
                    await connection.SendMessageAsync(input);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    class Connection : IDisposable
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly EndPoint? _remoteEndPoint;
        private readonly Task _readingTask;
        private readonly Task _writingTask;
        private readonly Channel<string> _channel;

        public Connection(TcpClient client)
        {
            //клиент, да
            _client = client;
            //поток, для хранения получаемых и отправляемых данных
            _stream = client.GetStream();
            //удаленная конечная точка
            _remoteEndPoint = client.Client.RemoteEndPoint;
            //канал передачи данных
            _channel = Channel.CreateUnbounded<string>();
            
            //задачи/циклы чтения/записи
            _readingTask = RunReadingLoop();
            _writingTask = RunWritingLoop();
        }
        //цикл чтения 
        private async Task RunReadingLoop()
        {
            try
            {
                byte[] headerBuffer = new byte[4];
                while (true)
                {
                    int bytesReceived = await _stream.ReadAsync(headerBuffer, 0, headerBuffer.Length);
                    if (bytesReceived != 4)
                        break;
                    int length = BinaryPrimitives.ReadInt32LittleEndian(headerBuffer);
                    byte[] buffer = new byte[length];
                    int count = 0;
                    while (count < length)
                    {
                        bytesReceived = await _stream.ReadAsync(buffer, count, buffer.Length - count);
                        count += bytesReceived;
                    }
                    string message = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine($"<< {_remoteEndPoint}: {message}");
                }
                Console.WriteLine($"Сервер закрыл соединение.");
                _stream.Close();
            }
            catch (IOException)
            {
                Console.WriteLine($"Подключение закрыто.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }
        }
        //отправить сообщение
        public async Task SendMessageAsync(string message)
        {
            Console.WriteLine($">> {_remoteEndPoint}: {message}");
            await _channel.Writer.WriteAsync(message);
        }

        //цкил записи
        private async Task RunWritingLoop()
        {
            //заголовок
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

        bool disposed;
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