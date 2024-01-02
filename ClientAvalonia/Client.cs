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


namespace ClientAvalonia
{
    class Connection : IDisposable
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly EndPoint? _remoteEndPoint;
        private readonly Task _readingTask;
        private readonly Task _writingTask;
        private readonly Channel<string> _channel;
        private readonly Channel<byte[]> _channelForImage;

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
            _channelForImage = Channel.CreateUnbounded<byte[]>();

            //задачи/циклы чтения/записи
            _readingTask = RunReadingLoop();
            _writingTask = RunWritingLoop();
        }
        //цикл чтения 
        private async Task RunReadingLoop()
        {
            try
            {
                //в заголовке находится длина отправляемого/принимаемого сообщения
                //int занимает 4 байта
                byte[] lengthMessageBytes = new byte[4];
                while (true)
                {

                    //читаем "заголовок", и также пропускаем первые 4 байта
                    int bytesReceived = await _stream.ReadAsync(lengthMessageBytes, 0, lengthMessageBytes.Length);
                    //если вдруг будет меньше или больше(вот тут я даже не знаю как), то закрываем
                    if (bytesReceived != 4)
                        break;
                    //получаем размер сообщения
                    int length = BinaryPrimitives.ReadInt32LittleEndian(lengthMessageBytes);


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
                    string headerQuery = Encoding.UTF8.GetString(headerQueryBytes);



                    //вот тут надо будет добавить что-то для выбора метода записи
                    //в зависимости от заголовка
                    //так, будет ясно что считвается, изображение или текст
                    //количество пропускаемых байт 
                    //з.ы. также и в сервере
                    int count = 0;
                    //буффер для принимаемого сообщения
                    byte[] buffer = new byte[length];
                    while (count < length)
                    {
                        bytesReceived = await _stream.ReadAsync(buffer, count, buffer.Length - count);
                        count += bytesReceived;
                    }
                    //байты в текст
                    string message = Encoding.UTF8.GetString(buffer);





                    //и отправляем клиенту(тут это просто вывод на экран)
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
        //отправить картинку
        public async Task SendMessageAsync(byte[] image)
        {
            //image = "request from server :  " + message + Environment.NewLine;
            await _channelForImage.Writer.WriteAsync(image);
        }

        //цкил записи
        private async Task RunWritingLoop()
        {
            //заголовок
            byte[] header = new byte[4];

            //заголовок запроса
            byte[] headerQueryLengthBytes = new byte[4];
            byte[] headerQueryBytes = Encoding.UTF8.GetBytes("Какой-то заголовок");

            await foreach (string message in _channel.Reader.ReadAllAsync())
            {
                //буфер сообщения + его длина
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                BinaryPrimitives.WriteInt32LittleEndian(header, buffer.Length);
                await _stream.WriteAsync(header, 0, header.Length); //длина сообщения

                //записываем длину заголовка, а также сам заголовок
                BinaryPrimitives.WriteInt32LittleEndian(headerQueryLengthBytes, headerQueryBytes.Length);
                await _stream.WriteAsync(headerQueryLengthBytes, 0, headerQueryLengthBytes.Length); //длина заголовка
                await _stream.WriteAsync(headerQueryBytes, 0, headerQueryBytes.Length); //пишем зоголовок

                //записываем само сообщение
                await _stream.WriteAsync(buffer, 0, buffer.Length);//содержимое сообщения
            }

            //тут изменить заголовок
            await foreach (byte[] image in _channelForImage.Reader.ReadAllAsync())
            {
                //буфер сообщения + его длина
                byte[] buffer = image;
                BinaryPrimitives.WriteInt32LittleEndian(header, buffer.Length);
                await _stream.WriteAsync(header, 0, header.Length); //длина сообщения

                //записываем длину заголовка, а также сам заголовок
                BinaryPrimitives.WriteInt32LittleEndian(headerQueryLengthBytes, headerQueryBytes.Length);
                await _stream.WriteAsync(headerQueryLengthBytes, 0, headerQueryLengthBytes.Length); //длина заголовка
                await _stream.WriteAsync(headerQueryBytes, 0, headerQueryBytes.Length); //пишем зоголовок

                //записываем само сообщение
                await _stream.WriteAsync(buffer, 0, buffer.Length);//содержимое сообщения
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
