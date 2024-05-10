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
    internal class AttendanceLog
    {
        public static async Task<AttendanceLog?> GetAttendanceLog(int id)
        {
            HttpClient client = new HttpClient();
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



        //получение всех логов с БД


        //добавить по датам

        public static async Task<IEnumerable<AttendanceLog>?> GetAttendanceLogs()
        {
            HttpClient client = new HttpClient();
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
        public static async Task<HttpStatusCode?> PostAttendanceLog(AttendanceLog AttendanceLog)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.PostAsJsonAsync(Connect.Connection + $"AttendanceLog", AttendanceLog);
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //обновление лога
        public static async Task<HttpStatusCode?> PutAttendanceLog(AttendanceLog AttendanceLog)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.PutAsJsonAsync(Connect.Connection + $"AttendanceLog", AttendanceLog);
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
