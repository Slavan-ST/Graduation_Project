using Helper.Models.DTO;
using Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Client.API
{
    internal static class Student
    {
        //тут будет получение и отправка на сервер


        //получение одного лога по id
        public static async Task<StudentDTO?> GetStudentAsync(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<StudentDTO>(Connect.Connection + $"Students/{id}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //получение всех логов с БД
        public static async Task<IEnumerable<StudentDTO>?> GetStudentsAsync()
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<StudentDTO>>(Connect.Connection + $"Students");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //удаление лога
        public static async Task<HttpStatusCode?> DeleteStudentAsync(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.DeleteAsync(Connect.Connection + $"Students/{id}");
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //создание лога
        public static async Task<HttpStatusCode?> PostStudentAsync(StudentDTO studentDTO)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.PostAsJsonAsync(Connect.Connection + $"Students", studentDTO);
                return response.StatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        //обновление лога
        public static async Task<HttpStatusCode?> PutPutStudentAsync(StudentDTO studentDTO)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.PutAsJsonAsync(Connect.Connection + $"Students", studentDTO);
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
