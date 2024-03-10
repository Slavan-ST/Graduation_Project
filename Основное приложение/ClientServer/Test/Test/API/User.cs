using Helper.Models.DTO;
using Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Client.API
{
    internal static class User
    {
        //тут будет получение и отправка на сервер



        //получение одного лога по id
        public static async Task<UserDTO?> GetUserAsync(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<UserDTO>(Connect.Connection + $"Users/{id}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //получение всех логов с БД
        public static async Task<IEnumerable<UserDTO>?> GetUsersAsync()
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<UserDTO>>(Connect.Connection + $"Users");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //удаление лога
        public static async Task<HttpStatusCode?> DeleteUserAsync(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.DeleteAsync(Connect.Connection + $"Users/{id}");
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //создание лога
        public static async Task<HttpStatusCode?> PostUserAsync(UserDTO userDTO)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.PostAsJsonAsync(Connect.Connection + $"Users", userDTO);
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //обновление лога
        public static async Task<HttpStatusCode?> PutUserAsync(UserDTO userDTO)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.PutAsJsonAsync(Connect.Connection + $"Users", userDTO);
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

    }
}
