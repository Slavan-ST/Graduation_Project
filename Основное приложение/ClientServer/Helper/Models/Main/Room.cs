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
    public class Room: Base
    {
        public string Number { get; set; } = "";

        public int CountStudents
        {
            get
            {
                if (Students == null)
                {
                    return 0;
                }
                return Students.Count();
            }
        }

        IEnumerable<Student>? _students = [];
        public IEnumerable<Student>? Students
        {
            get
            {
                if (_students != null)
                {
                    foreach (var stud in _students)
                    {
                        stud.Room = null;
                    }
                }
                return _students;
            }
            set
            {
                _students = value;
            }
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
