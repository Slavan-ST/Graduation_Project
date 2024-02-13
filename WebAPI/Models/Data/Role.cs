using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Models.Data
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public static string Admin { get; } = "Admin";
        public static string User { get; } = "User";
        public static string Guest { get; } = "Guest";
        public static string Moderator { get; } = "Moderator";
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
