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

    internal class RolesAPI
    {
        public static async Task<IEnumerable<Role>?> GetAsync()
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<Role>>(Connect.Connection + $"Roles");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        public static async Task<Role?> GetAsync(string name)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<Role>(Connect.Connection + $"Roles/{name}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        public static async Task<HttpStatusCode?> DeleteAsync(int id)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.DeleteAsync(Connect.Connection + $"Roles/{id}");
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        public static async Task<int?> PostAsync(Role role)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.PostAsJsonAsync(Connect.Connection + $"Roles", role);
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    await PutAsync(role);
                    return null;
                }
                else
                {
                    Debug.WriteLine("Code: " + response.StatusCode);
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
        public static async Task<HttpStatusCode?> PutAsync(Role role)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.PutAsJsonAsync(Connect.Connection + $"Roles", role);
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
