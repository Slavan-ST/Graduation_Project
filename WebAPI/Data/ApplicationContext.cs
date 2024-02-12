using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebAPI.Models.Data;

namespace WebAPI.Data
{
    public class ApplicationContext : DbContext
    {
        //коннект к БД
        static string _connectionString = @"Server = SlavanST\Slavan; Database = SystemO; User id = sa; Password = 123; TrustServerCertificate = True; ";

        public ApplicationContext()
        {

        }

        #region Таблицы

        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Marker> Markers { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<AttendanceLog> AttendanceLog { get; set; } = null!;

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
