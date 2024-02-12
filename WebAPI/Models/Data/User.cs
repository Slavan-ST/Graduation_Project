using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI.Models.Data
{
    public class User
    {
        public User()
        {

        }
        public int Id { get; set; }
        public string? Name { get; set; } = "";
        public string? Surname { get; set; } = "";
        public string? Patronymic { get; set; } = "";
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public byte[]? Image { get; set; }
        public int RoleId { get; set; }
        public Role? Role
        {
            get => new ApplicationContext().Roles.Where(x => x.Id == RoleId).FirstOrDefault();
        }
    }
}
