using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models
{
    /// <summary>
    /// Запрос серверу/ответ клиенту
    /// </summary>
    public class Query
    {
        public Query(string header, byte[] content)
        {
            Header = header;
            Content = content;
        }
        public Query(string header)
        {
            Header = header;
        }
        public Query()
        {

        }

        //тип запроса
        public string TypeQuery { get; set; }
        //статус ответа
        public string Status { get; set; }
        //дата
        public string Date { get; set; }
        //тип содержимого
        public string ContentType { get; set; }
        public string Header { get;} = "";
        public byte[]? Content { get;}

        public int LengthHeader { get;} = 0;
        public int LengthContent { get;} = 0;
    }
}
