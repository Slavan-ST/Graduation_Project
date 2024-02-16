using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public static string Admin { get; } = "Admin";
        public static string User { get; } = "User";
        public static string Guest { get; } = "Guest";
        public static string Moderator { get; } = "Moderator";
        [JsonIgnore]
        public virtual ICollection<User>? Users { get; set; }
    }
}
