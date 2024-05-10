using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.Net.NetworkInformation;

namespace Client
{
    public static class Connect
    {
        static Connect()
        {
            var config = new ConfigurationBuilder()
                .AddXmlFile("App.config")
                .Build();

            string? connect = config["Connect"];

            if (connect == null)
            {
                return;
            }

            _connection = connect;
        }
        static string _connection = "http://localhost:8080/";

        public static string Connection
        {
            get
            {
                if (!CheckInternetConnection(_connection))
                {
                    //тут пихай всё своё *************
                }
                return _connection;
            }
        }

        public static bool CheckInternetConnection(string connect)
        {
            try
            {
                Ping myPing = new Ping();
                byte[] buffer = new byte[32];
                int timeout = 1000; // Timeout in milliseconds
                PingOptions options = new PingOptions();
                PingReply reply = myPing.Send(connect, timeout, buffer, options);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
