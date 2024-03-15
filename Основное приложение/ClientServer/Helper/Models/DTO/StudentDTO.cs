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
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public RoomDTO? Room { get; set; }

        public StudentDTO() { }
        public StudentDTO(Student? student)
        {
            if (student == null)
            {
                return;
            }

            this.Name = student.Name;
            this.Surname = student.Surname;
            this.Patronymic = student.Patronymic;
            this.Room = new RoomDTO(student.Room);
        }
    }
}
