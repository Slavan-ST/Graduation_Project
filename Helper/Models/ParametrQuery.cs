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
        public ParametrQuery(string type,string name, byte[] content)
        {
            Type = type;
            Name = name;
            Content = content;
            Length = content.Length;
        }
        public ParametrQuery(string type,string name ,int length)
        {
            Type = type;
            Name = name;
            Length = length;
        }
        public string Name { get; set; }
        public int Length { get; } = 0;
        public string Type { get;}
        public byte[]? Content { get; set; }
    }
}
