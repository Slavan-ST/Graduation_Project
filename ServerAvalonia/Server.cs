
using Helper.Models;
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
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly EndPoint? _remoteEndPoint;
        private readonly Task _readingTask;
        private readonly Task _writingQueryTask;
        private readonly Action<Connection> _disposeCallback;
        private readonly Channel<Query> _channelForQuery;
        bool disposed;

        public Connection(TcpClient client, Action<Connection> disposeCallback)
        {
            _client = client;
            _stream = client.GetStream();
            _remoteEndPoint = client.Client.RemoteEndPoint;
            _disposeCallback = disposeCallback;

            _channelForQuery = Channel.CreateUnbounded<Query>();
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
                    Header header = new Header(headerQuery); //парсим

                    for (int i = 0; i < header.ParamsQuery.Count; i++)
                    {
                        byte[] buffer = new byte[header.ParamsQuery[i].Length];
                        bytesReceived = await _stream.ReadAsync(buffer, 0, buffer.Length);
                        header.ParamsQuery[i].Content = buffer;
                    }

                    SendAnswer(header);
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
        private async void SendAnswer(Header header)
        {

            List< ParametrQuery> parametrsClient = new List<ParametrQuery>();
            foreach (var p in header.ParamsQuery)
            {
                parametrsClient.Add(new ParametrQuery(p.Type,p.Name, p.Content!));
            }

            Query? answer = null;


            if (header.Type == "SELECT")
            {
                string text = "OK";
                string type = "params";           //потом поменять
                var parametrs = DataBase.Select(header.Text, parametrsClient);
                Content content = new Content(parametrs!);
                answer = new Query(new Header(type, text),content);
                answer.Header.ParamsQuery = new List<ParametrQuery>(parametrs!);
            }
            else if (header.Type == "UPDATE")
            {
                string text = "OK";
                string type = "text";           //потом поменять
                var s = DataBase.Update(header.Text, parametrsClient);
                Content content = new Content(new List<ParametrQuery>() { new ParametrQuery("string","text", Encoding.UTF8.GetBytes(s))});
                answer = new Query(new Header(type, text), content);
            }
            else if (header.Type == "DELETE")
            {
                string text = "OK";
                string type = "text";           //потом поменять
                var s = DataBase.Delete(header.Text, parametrsClient);
                Content content = new Content(new List<ParametrQuery>() { new ParametrQuery("text", "text", Encoding.UTF8.GetBytes(s)) });
                answer = new Query(new Header(type, text), content);
            }
            else if (header.Type == "CREATE")
            {
                string text = "OK";
                string type = "text";           //потом поменять
                var s = DataBase.Create(header.Text, parametrsClient);
                Content content = new Content(new List<ParametrQuery>() { new ParametrQuery("text", "text", Encoding.UTF8.GetBytes(s)) });
                answer = new Query(new Header(type, text), content);
            }
            else
            {
                string text = "OK";
                string type = "text";           //потом поменять
                var s = "no";
                Content content = new Content(new List<ParametrQuery>() { new ParametrQuery("text", "text", Encoding.UTF8.GetBytes(s)) });
                answer = new Query(new Header(type, text), content);
            }
            await SendMessageAsync(answer!);

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

