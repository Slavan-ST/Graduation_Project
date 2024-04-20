using Helper;
using Helper.Models.Main;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.API
{
    internal static class PurityRaidLogAPI
    {
        public static async Task<PurityRaidLog?> GetPurityRaidLog(int id)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<PurityRaidLog>(Connect.Connection + $"PurityRaidLog/{id}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<IEnumerable<PurityRaidLog>?> GetPurityRaidLogs()
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<PurityRaidLog>>(Connect.Connection + $"PurityRaidLog");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<IEnumerable<PurityRaidLog>?> GetPurityRaidLogsYear(int year)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<PurityRaidLog>>(Connect.Connection + $"PurityRaidLog/year:{year}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<IEnumerable<PurityRaidLog>?> GetPurityRaidLogsMonth(int year, int month)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<PurityRaidLog>>(Connect.Connection + $"PurityRaidLog/month:{month}.{year}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<IEnumerable<PurityRaidLog>?> GetPurityRaidLogsDay(int year, int month, int day)
        {
            HttpClient client =     HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<PurityRaidLog>>(Connect.Connection + $"PurityRaidLog/day:{day}.{month}.{year}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }


        public static async Task<HttpStatusCode?> DeletePurityRaidLog(int id)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.DeleteAsync(Connect.Connection + $"PurityRaidLog/{id}");
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //создание лога
        public static async Task<HttpStatusCode?> PostPurityRaidLog(PurityRaidLog purityRaidLog)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.PostAsJsonAsync(Connect.Connection + $"PurityRaidLog", purityRaidLog);
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //обновление лога
        public static async Task<HttpStatusCode?> PutPurityRaidLog(PurityRaidLog purityRaidLog)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.PutAsJsonAsync(Connect.Connection + $"PurityRaidLog", purityRaidLog);
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
