using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models.Data
{
    public class AttendanceLog
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int MarkerId { get; set; }
        public Marker? Marker { get; set; }
        public DateTime Date { get; set; }
    }
}
