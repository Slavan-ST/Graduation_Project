using Helper.Models.DTO;
using Helper.Models.Main;

namespace Helper.Converters
{
    public static class ConverterDTO
    {

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
                Role = userDTO.Role,
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
                RoleId = userDTO.Role!.Id,
                Password = userDTO.Password
            };
        }


    }
}
