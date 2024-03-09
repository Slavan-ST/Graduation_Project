using Helper;
using Helper.Models.DTO;
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
    internal static class AttendanceLog
    {
        //тут будет получение и отправка на сервер get, post,put,delete
        //AttendanceLog

        //Test
        //вариант 1 сделать каждый класс по отдельности

        //получение одного лога по id
        public static async Task<AttendanceLogDTO?> GetAttendanceLog(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<AttendanceLogDTO>(Connect.Connection + $"AttendanceLog/{id}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //получение всех логов с БД
        public static async Task<IEnumerable<AttendanceLogDTO>?> GetAttendanceLogs()
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<AttendanceLogDTO>>(Connect.Connection + $"AttendanceLog");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //удаление лога
        public static async Task<HttpStatusCode?> DeleteAttendanceLog(int id)
        {
            HttpClient client = new HttpClient();
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
        public static async Task<HttpStatusCode?> PostAttendanceLog(AttendanceLogDTO attendanceLogDTO)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.PostAsJsonAsync(Connect.Connection + $"AttendanceLog", attendanceLogDTO);
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //обновление лога
        public static async Task<HttpStatusCode?> PutAttendanceLog(AttendanceLogDTO attendanceLogDTO)
        {
            HttpClient client = new HttpClient();
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
