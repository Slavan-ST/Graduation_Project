using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models
{
    public class HeaderClient
    {
        public HeaderClient(string type, string contentType, string queryText, IEnumerable<string> paramsQuery)
        {
            TypeQuery = type;
            ContentType = contentType;
            QueryText = queryText;
            ParamsQuery.AddRange(paramsQuery);
        }
        public HeaderClient(string textHeader)
        {
            string[] lines = textHeader.Split("\n");
            TypeQuery = lines[0].Trim();
            ContentType = lines[1].Trim();
            QueryText = lines[2].Trim();
            ParamsQuery = new List<ParametrQuery>();
            string textParams = lines[3];
            List<string> paramsQueryNoForamt = new List<string>(textParams.Split(" "));
            foreach (var p in paramsQueryNoForamt)
            {
                ParamsQuery.Add(new ParametrQuery(p, "type"));
            }
        }
        public string TypeQuery { get; set; }
        public string ContentType { get; set; }
        public string QueryText { get; set; }
        public List<ParametrQuery> ParamsQuery { get; set; } = new List<ParametrQuery>();
        public string GetText()
        {
            string textParams = "";
            foreach(var p in ParamsQuery)
            {
                textParams += p + " " + "type";
            }
            //удалить последний пробел
            textParams = textParams.TrimEnd();

            return TypeQuery + Environment.NewLine +
                   ContentType + Environment.NewLine +
                   QueryText + Environment.NewLine + 
                   textParams;
        }
    }
}
