using Helper.Models.Main;
using System.Collections.Generic;

namespace Client.Models
{
    public class MarksOfStudents
    {
        public Student Student { get; set; } = new Student();
        public List<AttendanceLog> Logs { get; set; } = new List<AttendanceLog>();
    }
}
