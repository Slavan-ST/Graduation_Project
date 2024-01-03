using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models
{
    public class ParametrQuery
    {
        public ParametrQuery(string type, byte[] content)
        {
            Type = type;
            Content = content;
        }

        public string Type { get;}
        public byte[] Content { get;}
    }
}
