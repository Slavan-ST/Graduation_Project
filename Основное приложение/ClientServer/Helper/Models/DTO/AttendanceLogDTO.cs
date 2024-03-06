using Helper.Models.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Helper.Models.DTO
{
    public class AttendanceLogDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int MarkerId { get; set; }
        public DateTime Date { get; set; }

        public StudentDTO? Student { get; set; }
        public MarkerDTO? Marker { get; set; }

        public AttendanceLogDTO() { }
        public AttendanceLogDTO(AttendanceLog log)
        {
            this.Id = log.Id;
            this.StudentId = log.StudentId;
            this.MarkerId = log.MarkerId;
            this.Date = log.Date;

            this.Student = new StudentDTO(log.Student);
            this.Marker = new MarkerDTO(log.Marker);
        }
    }
}
