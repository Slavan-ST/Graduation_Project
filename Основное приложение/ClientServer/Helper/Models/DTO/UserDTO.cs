using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    }
}
