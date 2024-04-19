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
    public class Student: Base
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;


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

        public IEnumerable<AttendanceLog>? AttendanceLogs { get; set; }
        public string FIO
        {
            get => $"{Surname} {Name} {Patronymic}";
        }

        public override string ToString()
        {
            return FIO;
        }
    }
}
