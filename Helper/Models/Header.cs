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
        #region конструкторы
        /// <summary>
        /// Заголовок запроса/ответа
        /// </summary>
        /// <param name="type">тип</param>
        /// <param name="text">текст (ответ, запрос)</param>
        public Header(string type, string text)
        {
            Type = type;
            Text = text;
        }


        /// <summary>
        /// парсим заголовок из строки
        /// </summary>
        /// <param name="textHeader"></param>
        public Header(string textHeader)
        {
            //получаем каждую строку заголовка и начинаем парсить
            string[] lines = textHeader.Split("\n");
            Type= lines[0].Trim();
            Text = lines[1].Trim();
            ParamsQuery = new List<ParametrQuery>();
            string textParams = lines[2];
            //если параметры есть
            if (textParams != "null")
            {
                //неформатированная строка параметров
                List<string> paramsQueryNoForamt = new List<string>(textParams.Split(";"));
                //парсим параметры
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
        #endregion
        #region Свойства
        public string Type { get; set; }
        public string Text { get; set; }
        public int LengthHeader { get; set; } = 0;
        public List<ParametrQuery> ParamsQuery { get; set; } = new List<ParametrQuery>();
        #endregion
        /// <summary>
        /// Заголовок в текст
        /// </summary>
        /// <returns>текст, который при помощи конструктора можно конвертировать в заголовок</returns>
        public string GetText()
        {
            //для хранения характеристик параметров
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
            //если параметров нет, записываем null
            if (ParamsQuery.Count == 0)
            {
                textParams = "null";
            }
            //возвращаем
            return Type + Environment.NewLine +
                   Text + Environment.NewLine +
                   textParams;
        }
    }
}
