using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAvalonia.Models
{
    public class ParameterQuery
    {
        public ParameterQuery(string name, object content)
        {
            Name = name;
            Content = content;
        }
        public string Name { get; set; } = "no name";
        public object Content { get; set; }
    }
}
