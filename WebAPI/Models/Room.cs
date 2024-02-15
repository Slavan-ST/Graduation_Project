using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; } = "";
        [JsonIgnore]
        public List<Student> Students { get; set; } = new();
    }
}
