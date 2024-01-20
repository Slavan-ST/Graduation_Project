using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models
{
    public class User : BaseModel
    {
        public string FIO { get; set; } = "";
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public int RoleId { get; set; }
        public byte[]? Image { get; set; }
    }
}
