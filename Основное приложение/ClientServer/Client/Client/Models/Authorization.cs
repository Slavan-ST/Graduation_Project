using Avalonia;
using Client.API;
using Client.Services;
using Helper;
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
    internal static class Authorization
    {
        internal static async Task<Helper.Models.Main.User?> AuthorizationUser(string? login, string? password)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<Helper.Models.Main.User?>(Connect.Connection + $"login?login={login}&password={password}");
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
