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
        public HeaderClient(string type, string contentType, string queryText)
        {
            TypeQuery = type;
            QueryText = queryText;
        }


        /// <summary>
        /// парсим заголовок из строки
        /// </summary>
        /// <param name="textHeader"></param>
        public HeaderClient(string textHeader)
        {
            string[] lines = textHeader.Split("\n");
            TypeQuery = lines[0].Trim();
            QueryText = lines[1].Trim();
            ParamsQuery = new List<ParametrQuery>();
            string textParams = lines[2];

            List<string> paramsQueryNoForamt = new List<string>(textParams.Split(";"));
            foreach (var p in paramsQueryNoForamt)
            {
                string[] typeAndLength = p.Split(" ");

                string type = typeAndLength[0];
                int length = int.Parse(typeAndLength[1]);

                ParamsQuery.Add(new ParametrQuery(type, length));
            }
        }
        public string TypeQuery { get; set; }
        public string QueryText { get; set; }
        public int LengthHeader { get; set; } = 0;
        public List<ParametrQuery> ParamsQuery { get; set; } = new List<ParametrQuery>();
        public string GetText()
        {
            string textParams = "";
            for (int i = 0; i < ParamsQuery.Count; i++)
            {
                ParametrQuery? p = ParamsQuery[i];
                textParams += p.Type + " " + p.Length;
                if (i == ParamsQuery.Count - 1)
                {
                    textParams += ";";
                }
            }

            return TypeQuery + Environment.NewLine +
                   QueryText + Environment.NewLine + 
                   textParams;
        }
    }
}
