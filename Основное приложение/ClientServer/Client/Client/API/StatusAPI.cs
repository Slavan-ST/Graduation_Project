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
    internal class StatusAPI
    {
        public static async Task<IEnumerable<Status>?> GetStatusesAsync()
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<Status>>(Connect.Connection + $"Statuses");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        public static async Task<HttpStatusCode?> DeleteStatusAsync(int id)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.DeleteAsync(Connect.Connection + $"Statuses/{id}");
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        public static async Task<int?> PostStatusAsync(Status status)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.PostAsJsonAsync(Connect.Connection + $"Statuses", status);
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    await PutStatusAsync(status);
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
        public static async Task<HttpStatusCode?> PutStatusAsync(Status status)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.PutAsJsonAsync(Connect.Connection + $"Statuses", status);
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
