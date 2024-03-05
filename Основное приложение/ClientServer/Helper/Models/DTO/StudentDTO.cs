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
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public int RoomId { get; set; }
        [JsonIgnore]
        public Room? Room { get; set; }
        public StudentDTO() { }
        public StudentDTO(Student student)
        {
            this.Id = student.Id;
            this.Name = student.Name;
            this.Surname = student.Surname;
            this.Patronymic = student.Patronymic;
            this.RoomId = student.RoomId;
            this.Room = student.Room;
        }
    }
}
