// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Helper;
using Helper.Models.DTO;
using Helper.Models.Main;

namespace Client.API
{
    internal static class AttendanceLogAPI
    {
        public static async Task<AttendanceLog?> GetAttendanceLog(int id)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<AttendanceLog>(Connect.Connection + $"AttendanceLog/{id}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<IEnumerable<AttendanceLog>?> GetAttendanceLogs()
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<AttendanceLog>>(Connect.Connection + $"AttendanceLog");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<IEnumerable<AttendanceLog>?> GetAttendanceLogsYear(int year)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<AttendanceLog>>(Connect.Connection + $"AttendanceLog/year:{year}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<IEnumerable<AttendanceLog>?> GetAttendanceLogsMonth(int year, int month)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<AttendanceLog>>(Connect.Connection + $"AttendanceLog/month:{month}.{year}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<IEnumerable<AttendanceLog>?> GetAttendanceLogsDay(int year, int month, int day)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<AttendanceLog>>(Connect.Connection + $"AttendanceLog/day:{day}.{month}.{year}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }


        public static async Task<HttpStatusCode?> DeleteAttendanceLog(int id)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.DeleteAsync(Connect.Connection + $"AttendanceLog/{id}");
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //создание лога
        public static async Task<int?> PostAttendanceLog(AttendanceLog attendanceLogDTO)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.PostAsJsonAsync(Connect.Connection + $"AttendanceLog", attendanceLogDTO);

                //если лог уже существует, то просто обновляем старый
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    await PutAttendanceLog(attendanceLogDTO);
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
        //обновление лога
        public static async Task<HttpStatusCode?> PutAttendanceLog(AttendanceLog attendanceLogDTO)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.PutAsJsonAsync(Connect.Connection + $"AttendanceLog", attendanceLogDTO);
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
