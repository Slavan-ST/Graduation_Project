using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models
{
    public class AttendanceLog:BaseModel
    {
        public int RoomId { get; set; }
        public int StudentId { get; set; }
        public int MarkerId { get; set; }
        public DateTime Date { get; set; }
    }
}
