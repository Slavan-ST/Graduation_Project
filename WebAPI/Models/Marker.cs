using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Marker
    {
        public int Id { get; set; }
        public string Char { get; set; } = "";
        [JsonIgnore]
        public List<AttendanceLog> AttendanceLogs { get; set; } = new();
    }
}
