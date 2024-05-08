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
    public class StudentDTO: Base
    {
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
            this.DateBirthday = student.DateBirthday;
            this.Phone = student.Phone;
            this.Address = student.Phone;
            this.RepresentativePhone = student.RepresentativePhone;
            this.RepresentativePatronymic = student.RepresentativePatronymic;
            this.RepresentativeName = student.RepresentativeName;
            this.RepresentativeSurname = student.RepresentativeSurname;
            this.Gender = student.Gender;
            this.Room = student.Room;
            this.Group = student.Group;           
            this.Status = student.Status;
        }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public DateTime DateBirthday { get; set; }

        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string RepresentativeName { get; set; } = string.Empty;
        public string RepresentativeSurname { get; set; } = string.Empty;
        public string RepresentativePatronymic { get; set; } = string.Empty;
        public string RepresentativePhone { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty; //1 символ


        public int RoomId { get; set; }
        public int GroupId { get; set; }
        public int StatusId { get; set; }


        public Room? Room { get; set; }
        public Group? Group { get; set; }
        public Status? Status { get; set; }

        public string FIO
        {
            get => $"{Surname} {Name} {Patronymic}";
        }
        public int Age
        {
            get
            {
                return (DateTime.Now - DateBirthday).Hours / (24 * 365);
            }
        }
        public override string ToString()
        {
            return FIO;
        }
    }
}
