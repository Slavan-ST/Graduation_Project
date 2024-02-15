using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class AttendanceLog
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int MarkerId { get; set; }
        public DateTime Date { get; set; }

        [JsonIgnore]
        public Student? Student { get; set; }
        [JsonIgnore]
        public Marker? Marker { get; set; }
    }
}
