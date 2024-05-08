using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ReactiveUI;

namespace Helper.Models.Main
{
    public class AttendanceLog : Base
    {
        public AttendanceLog() 
        {

        }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public string Marker { get; set; } = string.Empty;

        Student? _student;
        public Student? Student
        {
            get
            {
                if (_student == null)
                {
                    return null;
                }
                _student.AttendanceLogs = null;
                return _student;
            }
            set
            {
                _student = value;
            }
        }
    }
}
