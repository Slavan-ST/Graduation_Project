using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Helper.Models.Main;

namespace Helper.Data
{
    public class ApplicationContext : DbContext
    {
        //коннект к БД
        static string _connectionString = @"Server = SLAVANST\192.168.0.2,54163; Database = SystemO; User id = sa; Password = 123; TrustServerCertificate = True; ";

        public ApplicationContext()
        {

        }

        //Таблицы
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<AttendanceLog> AttendanceLog { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
        }
    }
}
