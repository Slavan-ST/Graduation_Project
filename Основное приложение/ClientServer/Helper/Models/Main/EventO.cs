using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models.Main
{
    public class EventO : Base
    {
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Organizer { get; set; }
        public DateTime Date { get; set; }
    }
}
