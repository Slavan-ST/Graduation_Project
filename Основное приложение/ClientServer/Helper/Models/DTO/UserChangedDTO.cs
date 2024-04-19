using Helper.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models.DTO
{
    //эту ДТО будет отправлять клиент при изменении пользователя (эта ДТО включает в себя пароль)
    public class UserChangedDTO:Base
    {
        public string? Name { get; set; } = "";
        public string? Surname { get; set; } = "";
        public string? Patronymic { get; set; } = "";
        public string Password { get; set; } = "";
        public string Login { get; set; } = "";
        public byte[]? Image { get; set; }
        public Role? Role { get; set; }

        public UserChangedDTO() { }
        public UserChangedDTO(User? user)
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
            this.Password = user.Password;
        }
    }
}
