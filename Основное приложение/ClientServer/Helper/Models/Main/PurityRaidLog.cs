using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Helper.Models.Main
{
    public class PurityRaidLog: Base
    {
        public int RoomId { get; set; }
        public DateTime Date { get; set; }
        public string Marker { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Room? Room { get; set; }
        public string ShortDate { get => Date.ToShortDateString(); }
    }
}
