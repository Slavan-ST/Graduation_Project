using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.API
{
    public static class HttpClientSingleton
    {
        static HttpClient _client = new HttpClient();
        public static HttpClient Client
        {
            get => _client;
        }
    }
}
