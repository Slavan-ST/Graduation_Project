using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAvalonia.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public static string Admin { get; } = "Admin";
        public static string User { get; } = "User";
        public static string Guest { get; } = "Guest";
        public static string Moderator { get; } = "Moderator";
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
