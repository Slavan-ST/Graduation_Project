using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Helper.Models.DTO;
using Helper;
using Helper.Models.Main;
using MsBox.Avalonia;

namespace Client.API
{
    internal static class StudentAPI
    {
        public static async Task<Student?> GetStudentAsync(string fio, string room)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<Student>(Connect.Connection + $"Students/{room}/{fio}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }


        //добавить из обновы
        public static async Task<IEnumerable<Student>?> GetStudentsAsync()
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<Student>>(Connect.Connection + $"Students");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
        public static async Task<HttpStatusCode?> DeleteStudentAsync(int id)
        {
            HttpClient client = HttpClientSingleton.Client;
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
        public static async Task<int?> PostStudentAsync(Student studentDTO)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.PostAsJsonAsync(Connect.Connection + $"Students", studentDTO);

                //если студент уже существует, то обновим его
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    await PutStudentAsync(studentDTO);
                    return null;
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    await PutStudentAsync(studentDTO);
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
        public static async Task<HttpStatusCode?> PutStudentAsync(Student studentDTO)
        {
            HttpClient client = HttpClientSingleton.Client;
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
