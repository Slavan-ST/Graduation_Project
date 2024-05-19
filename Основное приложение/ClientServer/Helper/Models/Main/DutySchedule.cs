using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Helper.Models.Main
{
    public class DutySchedule : Base
    {
        public int? StudentId { get; set; }
        public DateTime Date { get; set; }
        public string? ShortDate 
        { 
            get => Date.ToShortDateString(); 
        }


        Student? _student;
        public Student? Student
        {
            get
            {
                if (_student == null)
                {
                    return null;
                }
                _student.DutySchedules = null;
                return _student;
            }
            set
            {
                _student = value;
            }
        }
    }
}
