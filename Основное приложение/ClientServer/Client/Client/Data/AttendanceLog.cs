using Helper;
using Helper.Models.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Data
{
    internal static class AttendanceLog
    {
        //тут будет получение и отправка на сервер get, post,put,delete
        //AttendanceLog

        //Test
        //вариант 1 сделать каждый класс по отдельности
        //вариант 2 сделать универсальный через параметры(мб не очень надёжно)

        public static async Task<AttendanceLogDTO?> GetAttendanceLog(params (string name, string value)[] pars)//id и прочее
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<AttendanceLogDTO>(Connect.Connection + $"AttendanceLog");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
    }
}
