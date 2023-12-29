using ClientAvalonia.Services;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace ClientAvalonia.Models
{
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
                //в заголовке находится длина отправляемого/принимаемого сообщения....
                //или нет....а ввообще да
                //int занимает 4 байта
                byte[] headerBuffer = new byte[4];
                while (true)
                {
                    //читаем "заголовок", и также пропускаем первые 4 байта
                    int bytesReceived = await _stream.ReadAsync(headerBuffer, 0, headerBuffer.Length);
                    //если вдруг будет меньше(вот тут я даже не знаю как), то закрываем
                    if (bytesReceived != 4)
                        break;
                    
                    //получаем размер сообщения
                    int length = BinaryPrimitives.ReadInt32LittleEndian(headerBuffer);
                    //буффер для принимаемого сообщения
                    byte[] buffer = new byte[length];
                    int count = 0;
                    while (count < length)
                    {
                        bytesReceived = await _stream.ReadAsync(buffer, count, buffer.Length - count);
                        count += bytesReceived;
                    }
                    //байты в текст
                    string message = Encoding.UTF8.GetString(buffer);
                    //и отправляем клиенту
                    Temp.MainViewModel.Answer = message;
                }
                _stream.Close();
            }
            catch (IOException)
            {
                Debug.WriteLine($"Подключение закрыто.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }
        }
        //отправить сообщение
        public async Task SendMessageAsync(string message)
        {
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
