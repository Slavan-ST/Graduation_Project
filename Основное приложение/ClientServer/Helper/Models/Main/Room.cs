using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Helper.Models.Main
{
    public class Room: Base
    {
        public string Number { get; set; } = "";

        public override string ToString()
        {
            return Number;
        }
    }
}
