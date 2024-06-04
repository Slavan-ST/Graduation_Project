using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Services
{
    public static class ConsoleAPI
    {
        public static async Task Message(string message)
        {
            await new HttpClient().GetAsync(Connect.Connection + $@"Message/{message}");
        }
        public static async void Message(object? message)
        {
            await new HttpClient().GetAsync(Connect.Connection + $@"Message/{(string)message}");
        }
    }
}
