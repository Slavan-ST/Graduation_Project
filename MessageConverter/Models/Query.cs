using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models
{
    /// <summary>
    /// Запрос серверу
    /// </summary>
    public class Query
    {
        public Query(Header header, Content? content = null)
        {
            Header = header;
            Content = content;
        }
        public Header Header { get;}
        public Content? Content { get;}
    }
}
