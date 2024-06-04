using ReactiveUI.Fody.Helpers;

namespace Helper.Models.Main
{
    public class User : Base
    {
        public string? Name { get; set; } = "";
        public string? Surname { get; set; } = "";
        public string? Patronymic { get; set; } = "";
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public byte[]? Image { get; set; }
        [Reactive]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

    }
}
