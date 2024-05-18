using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Helper.Models.Main;

namespace Helper
{
    public class JsonConverter
    {
        public static string GetJson(object obj)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var myReferenceHandler = new MyReferenceHandler();
            options.ReferenceHandler = myReferenceHandler;

            string json = "";
            json = JsonSerializer.Serialize(obj, options);

            // Reset after serializing to avoid out of bounds memory growth in the resolver.
            myReferenceHandler.Reset();
            return json;
        }
        public static T? FromJson<T>(string json = "")
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var myReferenceHandler = new MyReferenceHandler();
            options.ReferenceHandler = myReferenceHandler;


            var test = JsonSerializer.Deserialize<T>(json, options);

            // Reset after serializing to avoid out of bounds memory growth in the resolver.
            myReferenceHandler.Reset();
            return test;
        }
    }
}
