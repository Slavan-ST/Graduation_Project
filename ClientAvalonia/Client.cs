using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia;
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text.Json;
using System.Net.Http.Json;
using System.Threading;
using System.Text.Json.Serialization;


namespace ClientAvalonia
{
    public class TestClass
    {
        public TestClass(){}
        [JsonConstructor]
        public TestClass(string name, string lName, byte[] image)
        {
            Name = name;
            Image = image;
            LName = lName;
        }
        public string Name { get; set; } = "not found!";
        public string LName { get; set; } = "not found!";
        public byte[] Image { get; set; }
        public Bitmap? GetImage()
        {
            using var stream = new MemoryStream(Image);
            return new Bitmap(stream);
        }
        public void ImageToBytes(Bitmap image)
        {
            using var stream = new MemoryStream();
            image.Save(stream);
            Image = stream.ToArray();
        }
    }
    public class Client
    {

        static HttpClient? _httpClient;

        private static void WriteInDB()
        {

        }
        public static async void Start()
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2)
            };
            _httpClient = new HttpClient(socketsHandler);

            //сериализуем объект
            string testJson = JsonSerializer.Serialize(new TestClass()
            {
                LName = "Иванович",
                Name = "Иван",
                Image = await Helper.Services.FileDialog.OpenImage()
            });
            byte[] buffer  = Encoding.ASCII.GetBytes(testJson);


            // определяем данные запроса
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://127.0.0.1:1111/connection/");
            request.Content = new ByteArrayContent(buffer); //тут или stream или byte


            //отправляем сообщение и получаем ответ
            using HttpResponseMessage response = await _httpClient.SendAsync(request);


            // просматриваем данные ответа
            // статус
            Debug.WriteLine($"Status: {response.StatusCode}\n");

            // содержимое ответа
            string content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"Content:{content}");
        }
    }
}
