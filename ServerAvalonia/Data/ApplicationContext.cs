using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAvalonia.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source = 192.168.0.2, 62582; Databese = TestDB; User id = sa; Password = 123;");
        }
    }
}
