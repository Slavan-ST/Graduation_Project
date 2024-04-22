using Avalonia;
using Client.API;
using Helper;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    internal class Authorization
    {
        static Authorization authorization = new Authorization();
        internal static Authorization GetAuthorization()
        {
            return authorization;
        
        }
        private Authorization()
        {
            //это надо, так как не может быть одновременно и студент и сотрудник
            this.WhenAnyValue(x => x.IsEmployee).Subscribe(x => IsStudent = !IsEmployee);
            this.WhenAnyValue(x => x.IsStudent).Subscribe(x => IsEmployee = !IsStudent);
        }

        internal static async Task<Helper.Models.Main.User?> AuthorizationUser(string? login, string? password)
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetFromJsonAsync<Helper.Models.Main.User?>(Connect.Connection + $"login?login={login}&password={password}");
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        internal bool IsEmployee { get; set; } = false;
        internal bool IsStudent { get; set; } = false;
    }
}
