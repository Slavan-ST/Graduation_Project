using ClientAvalonia.Services;
using Helper.Models;
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
    public class Connection : IDisposable
    {
        #region поля и конструктор
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly EndPoint? _remoteEndPoint;
        private readonly Task _readingTask;
        private readonly Task _writingQueryTask;
        private readonly Channel<Query> _channelForQuery;

        public Connection(TcpClient client)
        {
            //клиент, да
            _client = client;
            //поток, для хранения получаемых и отправляемых данных
            _stream = client.GetStream();
            //удаленная конечная точка
            _remoteEndPoint = client.Client.RemoteEndPoint;
            //канал передачи данных
            _channelForQuery = Channel.CreateUnbounded<Query>();

            //задачи/циклы чтения/записи
            _readingTask = RunReadingLoop();
            _writingQueryTask = RunWritingQueryLoop();
        }
        #endregion
        #region чтение получаемых сообщений
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


                    //количество пропускаемых байт 
                    int count = 0;
                    //буффер для принимаемого сообщения
                    byte[] buffer = new byte[length];
                    while (count < length)
                    {
                        bytesReceived = await _stream.ReadAsync(buffer, count, buffer.Length - count);
                        count += bytesReceived;
                    }


                    //вот тут надо будет добавить что-то для выбора метода записи
                    //в зависимости от заголовка
                    //так, будет ясно что считвается, изображение или текст
                    //з.ы. также и в сервере
                    //в зависимости от заголовка конвертить байты из buffer в нужный нам тип
                    
                    //байты в текст                    
                    string message = Encoding.UTF8.GetString(buffer);       //
                    //и отправляем клиенту(тут это просто вывод на экран)   // и вот это заменить 
                    Temp.MainViewModel.Answer = message;                    //





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
        #endregion
        #region запись и отправка сообщений
        //отправить запрос
        public async Task SendMessageAsync(Query query)
        {
            await _channelForQuery.Writer.WriteAsync(query);
        }
        //цикл записи query
        private async Task RunWritingQueryLoop()
        {
            //тут изменить заголовок
            await foreach (Query query in _channelForQuery.Reader.ReadAllAsync())
            {
                //заголовок запроса
                byte[] headerQueryLengthBytes = new byte[4];
                byte[] headerQueryBytes = Encoding.UTF8.GetBytes(query.Header.GetText());

                //записываем длину заголовка, а также сам заголовок
                BinaryPrimitives.WriteInt32LittleEndian(headerQueryLengthBytes, headerQueryBytes.Length);
                await _stream.WriteAsync(headerQueryLengthBytes, 0, headerQueryLengthBytes.Length); //длина заголовка
                await _stream.WriteAsync(headerQueryBytes, 0, headerQueryBytes.Length); //пишем зоголовок

                //записываем параметры
                foreach (var p in query.Content.ParametrQueries)
                {
                    //длина параметра
                    byte[] lengthContent = new byte[4];
                    byte[] buffer = p.Content;
                    BinaryPrimitives.WriteInt32LittleEndian(lengthContent, buffer.Length);  //длина сообщения
                    await _stream.WriteAsync(lengthContent, 0, lengthContent.Length);       //длина сообщения

                    //содержимое параметра
                    await _stream.WriteAsync(buffer, 0, buffer.Length);//содержимое сообщения
                }
            }
        }
        #endregion
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
                _channelForQuery.Writer.Complete();
                _stream.Close();
                //ожидаем завершение задач чтения/записи
                Task.WaitAll(_readingTask, _writingQueryTask);
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
