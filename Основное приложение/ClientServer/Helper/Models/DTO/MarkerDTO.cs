using Helper.Models.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Helper.Models.DTO
{
    public class MarkerDTO
    {
        public int Id { get; set; }
        public string Char { get; set; } = "";
        [JsonIgnore]
        public List<AttendanceLog> AttendanceLogs { get; set; } = new();
    }
}
