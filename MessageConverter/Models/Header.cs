using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models
{
    public class Header
    {
        public Header(string type, string queryText)
        {
            Type = type;
            Text = queryText;
        }


        /// <summary>
        /// парсим заголовок из строки
        /// </summary>
        /// <param name="textHeader"></param>
        public Header(string textHeader)
        {
            string[] lines = textHeader.Split("\n");
            Type= lines[0].Trim();
            Text = lines[1].Trim();
            ParamsQuery = new List<ParametrQuery>();
            string textParams = lines[2];
            if (textParams != "null")
            {
                List<string> paramsQueryNoForamt = new List<string>(textParams.Split(";"));
                foreach (var p in paramsQueryNoForamt)
                {
                    string[] typeAndLength = p.Split(" ");

                    string type = typeAndLength[0].Trim();
                    string name = typeAndLength[1].Trim();
                    int length = int.Parse(typeAndLength[2].Trim());

                    ParamsQuery.Add(new ParametrQuery(type, name, length));
                }
            }
        }
        public string Type { get; set; }
        public string Text { get; set; }
        public int LengthHeader { get; set; } = 0;
        public List<ParametrQuery> ParamsQuery { get; set; } = new List<ParametrQuery>();
        public string GetText()
        {
            string textParams = "";
            for (int i = 0; i < ParamsQuery.Count; i++)
            {
                ParametrQuery? p = ParamsQuery[i];
                textParams += p.Type + " " + p.Name + " " + p.Length;
                if (i != ParamsQuery.Count - 1)
                {
                    textParams += ";";
                }
            }
            if (ParamsQuery.Count == 0)
            {
                textParams = "null";
            }
            return Type + Environment.NewLine +
                   Text + Environment.NewLine +
                   textParams;
        }
    }
}
