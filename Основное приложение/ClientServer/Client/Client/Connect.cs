using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using MsBox.Avalonia.Converters;
using System.Globalization;

namespace Client
{
    public static class Connect
    {
        public static string Connection
        {
            get
            {
                var config = new ConfigurationBuilder()
                    .AddXmlFile("App.config")
                    .Build();

                string? connect = config["Connect"];

                if (connect == null)
                {
                    return "http://localhost:8080/";
                }
                return connect;
            }
        }
    }
}
