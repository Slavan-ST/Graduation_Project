using Avalonia.Media.Imaging;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using ClientAvalonia.Services;
using Helper.Models;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace ClientAvalonia
{
    public class Client
    {
        static string _connect = @"https://localhost:7007";
        static HttpClient? _httpClient;

        public static async void Start()
        {
            //установить время жизни сокетов в 2 минуты, чтобы не засорять
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2)
            };
            _httpClient = new HttpClient(socketsHandler);


            var test = (await Get(typeof(string), _connect + "/student/2"))!;
            Debug.WriteLine("debug test: " + test.GetType());
        }

        private static async Task<object?> Get(Type type, string end_point)
        {
            // определяем данные запроса
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, end_point);
            //отправляем сообщение и получаем ответ
            var response = await _httpClient!.GetFromJsonAsync(end_point, type);

            Debug.WriteLine(response!.ToString());

            return response;
        }
        private static async Task<object?> Delete(Type type, string end_point)
        {
            // определяем данные запроса
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, end_point);
            //отправляем сообщение и получаем ответ
            return await _httpClient!.DeleteFromJsonAsync(end_point, type);

        }
        private static async Task<object?> Post(object obj, string end_point)
        {
            // определяем данные запроса
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, end_point);
            //отправляем сообщение и получаем ответ
            return await _httpClient!.PostAsJsonAsync(end_point, obj);
        }
        private static async Task<object?> Put(object obj, string end_point)
        {
            // определяем данные запроса
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, end_point);
            //отправляем сообщение и получаем ответ
            return await _httpClient!.PutAsJsonAsync(end_point, obj);
        }



        //id будет добавляться в строку подключения 
        public static async Task<Student?> GetStudent(int id)
        {
            return await Get(typeof(Student), "") as Student;
        }
    }
}
