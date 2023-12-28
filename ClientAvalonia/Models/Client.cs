using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ClientAvalonia.Models
{
}

namespace ClientAvalonia.Models.Client
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
