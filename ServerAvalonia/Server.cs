
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
    public class Server
    {
        static bool _isWorkServer = true;
        static HttpListener _server = new HttpListener();
        static string _serverConnection = "http://127.0.0.1:8888/connection/";//коннект по которому будет подключаться клиент

        public static async void Start()
        {
            _isWorkServer = true;
            _server = new HttpListener();
            _server.Prefixes.Add(_serverConnection);
            _server.Start(); // начинаем прослушивать входящие подключения
            while (_isWorkServer)
            {
                // получаем контекст
                var context = await _server.GetContextAsync();

                var request = context.Request;  // получаем данные запроса

                Debug.WriteLine($"адрес приложения: {request.LocalEndPoint}");
                Debug.WriteLine($"адрес клиента: {request.RemoteEndPoint}");
                Debug.WriteLine(request.RawUrl);
                Debug.WriteLine($"Запрошен адрес: {request.Url}");
                Debug.WriteLine("Заголовки запроса:");
                foreach (string item in request.Headers.Keys)
                {
                    Console.WriteLine($"{item}:{request.Headers[item]}");
                }

                var response = context.Response;    // получаем объект для установки ответа
                byte[] buffer = Encoding.UTF8.GetBytes("Hello METANIT");
                // получаем поток ответа и пишем в него ответ
                response.ContentLength64 = buffer.Length;
                using Stream output = response.OutputStream;
                // отправляем данные
                await output.WriteAsync(buffer);
                await output.FlushAsync();
            }

        }
        public static void Stop()
        {
            _isWorkServer = false;
            _server.Stop();
        }
    }


}

