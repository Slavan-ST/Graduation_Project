using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Models.DTO
{
    public class StudentDTO
    {
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public int RoomId { get; set; }
    }
}
