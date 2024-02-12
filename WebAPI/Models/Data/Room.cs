using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models.Data
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; } = "";
        public List<Student> Students { get; set; } = new();
    }
}
