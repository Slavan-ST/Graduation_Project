using Helper.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAvalonia.Models
{
    public class ApplicationContext:DbContext
    {
        //коннект к БД
        static string _connectionString = @"Server = SlavanST\Slavan; Database = SystemO; User id = sa; Password = 123; TrustServerCertificate = True; ";

        #region Таблицы
        //пример
        //public DbSet<Name_Class> Name_Table_in_SQLServer {get;set;}
        public DbSet<Student> Students { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
    public class Stud
    {

    }
}
