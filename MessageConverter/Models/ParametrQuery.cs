using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
            Length = content.Length;
        }
        public ParametrQuery(string type, int length)
        {
            Type = type;
            Length = length;
        }

        public int Length { get; } = 0;
        public string Type { get;}
        public byte[]? Content { get; set; }
    }
}
