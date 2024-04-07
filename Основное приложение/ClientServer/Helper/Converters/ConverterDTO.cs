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
        public static AttendanceLog? AttendanceLogFromDTO(AttendanceLogDTO? logDTO)
        {
            if (logDTO == null)
            {
                return null;
            }
            return new AttendanceLog()
            {
                Id = logDTO.Id,
                Date = logDTO.Date,
                Student = ConverterDTO.StudentFromDTO(logDTO.Student),
                Marker = logDTO.Marker
            };
        }
        public static Role? RoleFromDTO(RoleDTO? roleDTO)
        {
            if (roleDTO == null)
            {
                return null;
            }
            return new Role()
            {
                Id = roleDTO.Id,
                Name = roleDTO.Name
            };
        }
        public static Room? RoomFromDTO(RoomDTO? roomDTO) 
        {
            if (roomDTO == null)
            {
                return null;
            }
            return new Room()
            {
                Id = roomDTO.Id,
                Number = roomDTO.Number
            };
        }
        public static Student? StudentFromDTO(StudentDTO? studentDTO)
        {
            if (studentDTO == null)
            {
                return null;
            }
            return new Student()
            {
                Id = studentDTO.Id,
                Name = studentDTO.Name,
                Surname = studentDTO.Surname,
                Patronymic = studentDTO.Patronymic,
                Room = ConverterDTO.RoomFromDTO(studentDTO.Room)
            };
        }
        public static User UserFromDTO(UserDTO userDTO, string password)
        {
            return new User()
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Patronymic = userDTO.Patronymic,
                Login = userDTO.Login,
                Image = userDTO.Image,
                Role = ConverterDTO.RoleFromDTO(userDTO.Role),
                Password = password
            };
        }
        public static User UserFromChangedDTO(UserChangedDTO userDTO)
        {
            return new User()
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Patronymic = userDTO.Patronymic,
                Login = userDTO.Login,
                Image = userDTO.Image,
                Role = ConverterDTO.RoleFromDTO(userDTO.Role),
                Password = userDTO.Password
            };
        }


    }
}
