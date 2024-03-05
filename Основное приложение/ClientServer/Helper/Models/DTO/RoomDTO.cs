using Helper.Models.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Helper.Models.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Number { get; set; } = "";
        [JsonIgnore]
        public List<Student> Students { get; set; } = new();
    }
}
