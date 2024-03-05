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
    public class MarkerDTO
    {
        public int Id { get; set; }
        public string Char { get; set; } = "";

        public MarkerDTO() { }
        public MarkerDTO(Marker marker)
        {
            this.Id = marker.Id;
            this.Char = marker.Char;
        }
    }
}
