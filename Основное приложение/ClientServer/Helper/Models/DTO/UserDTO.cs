using Helper.Models.Main;

namespace Helper.Models.DTO
{
    public class UserDTO : Base
    {
        public string? Name { get; set; } = "";
        public string? Surname { get; set; } = "";
        public string? Patronymic { get; set; } = "";
        public string Login { get; set; } = "";
        public byte[]? Image { get; set; }
        public Role? Role { get; set; }

        public UserDTO() { }
        public UserDTO(User? user)
        {
            if (user == null)
            {
                return;
            }
            this.Id = user.Id;
            this.Name = user.Name;
            this.Surname = user.Surname;
            this.Login = user.Login;
            this.Image = user.Image;
            this.Role = user.Role;
        }
    }
}
