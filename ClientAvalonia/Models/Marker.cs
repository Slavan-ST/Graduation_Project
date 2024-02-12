using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAvalonia.Models
{
    public class Marker
    {
        public int Id { get; set; }
        public string Char { get; set; } = "";
        public List<AttendanceLog> AttendanceLogs { get; set; } = new();
    }
}
