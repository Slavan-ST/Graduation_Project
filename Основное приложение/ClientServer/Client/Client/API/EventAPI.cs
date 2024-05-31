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
    public class EventAPI
    {
        public static async Task<IEnumerable<EventO>?> GetsAsync()
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<EventO>>(Connect.Connection + $"Events");
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
                var response = await client.DeleteAsync(Connect.Connection + $"Events/{id}");
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        public static async Task<int?> PostAsync(EventO eventO)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.PostAsJsonAsync(Connect.Connection + $"Events", eventO);

                Debug.WriteLine("Code: " + response.StatusCode);
                //если лог уже существует, то просто обновляем старый
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    await PutAsync(eventO);
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
        public static async Task<HttpStatusCode?> PutAsync(EventO eventO)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.PutAsJsonAsync(Connect.Connection + $"Events", eventO);
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
