
using Avalonia.Remote.Protocol;
using ClientAvalonia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

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

                // получаем данные запроса
                var request = context.Request;  
                //считываем
                using var obj = new StreamReader(request.InputStream, request.ContentEncoding);
                //конвертим в текст
                string text = obj.ReadToEnd();
                Debug.WriteLine(text);
                var testObj = new TestClass()
                {
                    Name = "noname1",
                    LName = "lname2"
                };
                testObj.SetImageBitmap(Data.DataBase.ExecuteQueryTest()!);

                var content = JsonSerializer.Serialize(testObj);


                var response = context.Response;    // получаем объект для установки ответа
                byte[] buffer = Encoding.UTF8.GetBytes(content);
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

