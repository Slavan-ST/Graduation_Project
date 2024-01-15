using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia;
using ClientAvalonia.Services;
using Helper.Models;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.Http;
using System.Net.Mail;


namespace ClientAvalonia
{
    public class Client
    {
        static HttpClient? _httpClient;
        public static async void Start()
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2)
            };
            _httpClient = new HttpClient(socketsHandler);

            // использование HttpClient
        }
    }
}
