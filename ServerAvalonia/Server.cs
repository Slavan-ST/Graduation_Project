
using Helper.Models;
using Microsoft.EntityFrameworkCore.Storage.Json;
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
        static HttpListener _server;

        //адреса, обрабатываемые слушителем
        static List<string> _prefixes = new List<string>() 
        {
            "http://127.0.0.1:1111/connection/"  ////коннект по которому будет подключаться клиент, пока тесты
        };

        static Server()
        {
            _isWorkServer = true;
            _server = new HttpListener();
            foreach (var pref in _prefixes)
            {
                try
                {
                    _server.Prefixes.Add(pref);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);//тут может быть ошибка связанная с регистрацией этого адреса
                }
            }
        }
        public static async void Start()
        {
            _server.Start(); // начинаем прослушивать входящие подключения
            while (_isWorkServer)
            {
                // получаем контекст
                var context = await _server.GetContextAsync();

                var request = context.Request;  // получаем данные запроса
                //var requestContent = context.Response.
                Debug.WriteLine($"Type:{request.ContentType}");

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

