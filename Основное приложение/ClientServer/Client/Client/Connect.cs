using Client.API;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Client
{
    public static class Connect
    {
        static string _connection = "http://localhost:8080/";
        public static string Connection
        {
            get
            {
                return _connection;
            }
        }

    }
}
