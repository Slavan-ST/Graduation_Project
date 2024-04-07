using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Helper.Models.Main
{
    public class PurityRaidLog
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime Date { get; set; }
        public string Marker { get; set; } = string.Empty;
        public string? Description { get; set; }

        [JsonIgnore]
        public Room? Student { get; set; }
    }
}
