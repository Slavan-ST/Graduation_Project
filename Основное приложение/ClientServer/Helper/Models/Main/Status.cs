using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models.Main
{
    public class Status: Base
    {
        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return Name;
        }
    }
}
