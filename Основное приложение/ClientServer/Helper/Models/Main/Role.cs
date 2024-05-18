using Helper.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Helper.Models.Main
{
    public class Role: Base
    {
        public string Name { get; set; } = string.Empty;
        public override string ToString()
        {
            return this.Name;
        }

        public int CountUsers
        {
            get
            {
                if (Users == null)
                {
                    return 0;
                }
                return Users.Count();
            }
        }
        IEnumerable<User>? _user = new List<User>();
        public IEnumerable<User>? Users
        {
            get
            {
                if (_user != null)
                {
                    foreach (var user in _user)
                    {
                        user.Role = null;
                    }
                }
                return _user;
            }
            set
            {
                _user = value;
            }
        }
    }
}
