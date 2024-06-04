using System.Net.Http;

namespace Client.API
{
    public static class HttpClientSingleton
    {
        static HttpClient _client = new HttpClient();
        public static HttpClient Client
        {
            get
            {
                return _client;
            }
        }
    }
}
