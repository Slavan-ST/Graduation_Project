using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAvalonia.TestFromOld
{
    public class User
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "null";
        public int LVL { get; set; } = 0;
        public int Discount { get; set; } = 0;
        public byte[] Avatar { get; set; } = null;
    }
}
