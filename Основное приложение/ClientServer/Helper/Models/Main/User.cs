using Helper.Models.DTO;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Helper.Models.Main
{
    public class User: Base
    {
        public string? Name { get; set; } = "";
        public string? Surname { get; set; } = "";
        public string? Patronymic { get; set; } = "";
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public byte[]? Image { get; set; }
        [Reactive]
        public int RoleId { get; set; }
        public Role? Role{ get; set; }

    }
}
