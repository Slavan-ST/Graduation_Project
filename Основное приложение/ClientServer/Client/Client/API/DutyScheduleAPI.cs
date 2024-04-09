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
    internal static class DutyScheduleAPI
    {
        public static async Task<DutySchedule?> GetDutySchedule(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<DutySchedule>(Connect.Connection + $"DutySchedule/{id}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<IEnumerable<DutySchedule>?> GetDutySchedules()
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<DutySchedule>>(Connect.Connection + $"DutySchedule");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<IEnumerable<DutySchedule>?> GetDutySchedulesYear(int year)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<DutySchedule>>(Connect.Connection + $"DutySchedule/year:{year}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<IEnumerable<DutySchedule>?> GetDutySchedulesMonth(int year, int month)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<DutySchedule>>(Connect.Connection + $"DutySchedule/month:{month}.{year}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<IEnumerable<DutySchedule>?> GetDutySchedulesDay(int year, int month, int day)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<DutySchedule>>(Connect.Connection + $"DutySchedule/day:{day}.{month}.{year}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }


        public static async Task<HttpStatusCode?> DeleteDutySchedule(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.DeleteAsync(Connect.Connection + $"DutySchedule/{id}");
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //создание лога
        public static async Task<HttpStatusCode?> PostDutySchedule(DutySchedule dutySchedule)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.PostAsJsonAsync(Connect.Connection + $"DutySchedule", dutySchedule);
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //обновление лога
        public static async Task<HttpStatusCode?> PutDutySchedule(DutySchedule dutySchedule)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.PutAsJsonAsync(Connect.Connection + $"DutySchedule", dutySchedule);
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
