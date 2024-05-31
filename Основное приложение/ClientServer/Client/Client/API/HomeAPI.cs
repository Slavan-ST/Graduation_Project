using Helper.Models.DTO;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.API
{
    internal class HomeAPI
    {
        public static async Task<UserDTO?> SignIn(string login, string password)
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetFromJsonAsync<UserDTO>(Connect.Connection + $"Home/SignIn/?login={login}&password={password}");

                if (response == null)
                {
                    return response;
                }

                if (response.Name == "^not found^")
                {
                    return null;
                }

                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public static async Task<HttpStatusCode?> SignOut()
        {
            HttpClient client = HttpClientSingleton.Client;
            try
            {
                var response = await client.GetAsync(Connect.Connection + $"SignOut");
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
