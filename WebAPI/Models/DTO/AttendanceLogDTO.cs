using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebAPI.Models.Main;

namespace WebAPI.Models.DTO
{
    public class AttendanceLogDTO
    {
        public AttendanceLogDTO(AttendanceLog log)
        {

        }
        public StudentDTO Student { get; set; }
        public MarkerDTO Marker { get; set; }
        public DateTime Date { get; set; }
    }
}
