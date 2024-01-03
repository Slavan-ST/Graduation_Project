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
        public Query(HeaderClient header, Content content)
        {
            Header = header;
            Content = content;
        }
        public HeaderClient Header { get;}
        public Content Content { get;}
    }
}
