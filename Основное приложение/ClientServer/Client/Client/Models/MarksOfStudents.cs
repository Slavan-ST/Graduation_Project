using Helper.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class MarksOfStudents
    {
        public Student Student { get; set; } = new Student();
        public List<AttendanceLog> Logs { get; set; } = new List<AttendanceLog>();
    }
}
