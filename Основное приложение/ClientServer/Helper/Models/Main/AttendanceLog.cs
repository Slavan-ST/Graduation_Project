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
            this.WhenAnyValue(x => x.Student).Subscribe(x =>
            {
                StudentDTO = new StudentDTO(Student);
            });
        }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public string Marker { get; set; } = string.Empty;
        [JsonIgnore]
        public Student? Student { get; set; }
        public StudentDTO? StudentDTO { get; set; }
    }
}
