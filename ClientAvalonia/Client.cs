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
                    string headerQuery = Encoding.UTF8.GetString(headerQueryBytes);
                    HeaderClient header = new HeaderClient(headerQuery); //парсим

                    for (int i = 0; i < header.ParamsQuery.Count; i++)
                    {
                        byte[] buffer = new byte[header.ParamsQuery[i].Length];
                        bytesReceived = await _stream.ReadAsync(buffer, 0, buffer.Length);
                        header.ParamsQuery[i].Content = buffer;
                    }                           
                    
                    //и отправляем клиенту(тут это просто вывод на экран)   // и вот это заменить 
                    Temp.MainViewModel.Answer = header.Text;                    //

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
                foreach (var p in query.Content!.ParametrQueries)
                {
                    //содержимое параметра
                    byte[] buffer = p.Content!;
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
