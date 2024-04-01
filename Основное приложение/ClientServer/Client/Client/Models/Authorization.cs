using Avalonia;
using Client.Services;
using WebAPI;
using WebAPI.Models.Main;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public static class Authorization
    {
        public static async Task<User?> AuthorizationUser(string? login, string? password)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<User>(Connect.Connection + $"login?login={login}&password={password}");
                return response;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }
    }
}
