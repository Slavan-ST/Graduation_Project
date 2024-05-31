using Helper.Models.Main;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.API
{
    internal static class UserAPI
    {
        public static async Task<User?> GetUserAsync(string login)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<User>(Connect.Connection + $"Users/{login}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        public static async Task<IEnumerable<User>?> GetUsersAsync()
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<User>>(Connect.Connection + $"Users");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        public static async Task<HttpStatusCode?> DeleteUserAsync(string login)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.DeleteAsync(Connect.Connection + $"Users/{login}");
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        public static async Task<int?> PostUserAsync(User userDTO)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.PostAsJsonAsync(Connect.Connection + $"Users", userDTO);
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    await PutUserAsync(userDTO);
                    return null;
                }
                int result = await response.Content.ReadFromJsonAsync<int>();
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        public static async Task<HttpStatusCode?> PutUserAsync(User userDTO)
        {
            HttpClient client = HttpClientSingleton.Client;
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
        public static async Task<HttpStatusCode?> PutUserAsync(string login, string newPassword)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetAsync(Connect.Connection + $"Users/{login}&{newPassword}");
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
