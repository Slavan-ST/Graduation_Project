using Helper.Models.Main;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace WebAPI.Data
{
    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public ApplicationContext()
        {

        }

        //Таблицы
        /// <summary>
        /// Комнаты
        /// </summary>
        public DbSet<Room> Rooms { get; set; }
        /// <summary>
        /// Роли
        /// </summary>
        public DbSet<Role> Roles { get; set; }
        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Мероприятия
        /// </summary>
        public DbSet<EventO> Events { get; set; }
        /// <summary>
        /// Группы
        /// </summary>
        public DbSet<Group> Groups { get; set; }
        /// <summary>
        /// Статусы
        /// </summary>
        public DbSet<Status> Statuses { get; set; }
        /// <summary>
        /// Студенты
        /// </summary>
        public DbSet<Student> Students { get; set; }
        /// <summary>
        /// График дежурств
        /// </summary>
        public DbSet<DutySchedule> DutySchedule { get; set; }
        /// <summary>
        /// Журнал проверок нахождения в общежитие в ночное время
        /// </summary>
        public DbSet<AttendanceLog> AttendanceLog { get; set; }
        /// <summary>
        /// Журнал проверок чистоты
        /// </summary>
        public DbSet<PurityRaidLog> PurityRaidLogs { get; set; } 


        /// <summary>
        /// Первоначальная конфигурация контекста
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();


#if DEBUG
            string? connectionString = config.GetConnectionString("DebugConnection");
#else

            string connectionString = config.GetConnectionString("DefaultConnection");
#endif


            optionsBuilder.UseSqlServer(connectionString);
        }
        /// <summary>
        /// Настройка моделей
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            //modelBuilder.Entity<Student>().Ignore(x => x.AttendanceLogs!);
        }
    }
}
