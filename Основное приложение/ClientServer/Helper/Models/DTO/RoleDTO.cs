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
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public RoleDTO() { }
        public RoleDTO(Role? role)
        {
            if (role == null)
            {
                return;
            }
            this.Id = role.Id;
            this.Name = role.Name;
        }
    }
}
