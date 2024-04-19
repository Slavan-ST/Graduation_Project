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
    public class AttendanceLog : Base
    {
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public string Marker { get; set; } = string.Empty;

        [JsonIgnore]
        public Student? Student { get; set; }
    }
}
