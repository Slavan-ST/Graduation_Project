using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Helper.Models.Main;

namespace Helper.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; } = "";
        public string? Surname { get; set; } = "";
        public string? Patronymic { get; set; } = "";
        public string Login { get; set; } = "";
        public byte[]? Image { get; set; }
        public int RoleId { get; set; }

        public UserDTO() {}
        public UserDTO(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Surname = user.Surname;
            this.Login = user.Login;
            this.Image = user.Image;
            this.RoleId = user.RoleId;
        }
    }
}
