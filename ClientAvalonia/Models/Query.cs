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
        public Query(HeaderClient header, byte[] content)
        {
            Header = header;
            Content = content;
        }
        public Query(HeaderClient header)
        {
            Header = header;
        }
        public HeaderClient Header { get;}
        public byte[]? Content { get;}

        public int LengthHeader { get;} = 0;
        public int LengthContent { get;} = 0;
    }
}
