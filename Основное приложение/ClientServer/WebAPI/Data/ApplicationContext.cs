using Helper.Models.Main;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace WebAPI.Data
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
        public DbSet<EventO> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<DutySchedule> DutySchedule { get; set; } //График дежурств
        public DbSet<AttendanceLog> AttendanceLog { get; set; } //Журнал проверок нахождения в общежитие в ночное время
        public DbSet<PurityRaidLog> PurityRaidLogs { get; set; } //Журнал проверок чистоты


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
