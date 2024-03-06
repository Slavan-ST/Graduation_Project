using Helper.Models.DTO;
using Helper.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Converters
{
    public static class ConverterDTO
    {
        public static AttendanceLog AttendanceLogFromDTO(AttendanceLogDTO logDTO)
        {
            return new AttendanceLog()
            {
                Id = logDTO.Id,
                StudentId = logDTO.StudentId,
                MarkerId = logDTO.StudentId,
                Date = logDTO.Date,
                Student = logDTO.Student,
                Marker = logDTO.Marker
            };
        }
        public static Marker MarkerFromDTO(MarkerDTO markerDTO)
        {
            return new Marker()
            {
                Id = markerDTO.Id,
                Char = markerDTO.Char
            };
        }
        public static Role RoleFromDTO(RoleDTO roleDTO)
        {
            return new Role()
            {
                Id = roleDTO.Id,
                Name = roleDTO.Name
            };
        }
        public static Room RoomFromDTO(RoomDTO roomDTO) 
        {
            return new Room()
            {
                Id = roomDTO.Id,
                Number = roomDTO.Number
            };
        }
        public static Student StudentFromDTO(StudentDTO studentDTO)
        {
            return new Student()
            {
                Id = studentDTO.Id,
                Name = studentDTO.Name,
                Surname = studentDTO.Surname,
                Patronymic  = studentDTO.Patronymic,
                RoomId = studentDTO.RoomId,
                Room = studentDTO.Room
            };
        }

        public static User UserFromDTO(UserDTO userDTO)
        {
            return new User()
            {

            };
        }


    }
}
