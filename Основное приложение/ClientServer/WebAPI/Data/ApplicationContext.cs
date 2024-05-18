using Helper.Models.Main;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace WebAPI.Data
{
    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    public partial class ApplicationContext : DbContext
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

            string connectionString = config.GetConnectionString("Connection1080");
#endif


            optionsBuilder.UseSqlServer(connectionString);
        }
        /// <summary>
        /// Настройка моделей
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            //modelBuilder.Entity<Student>().Ignore(x => x.AttendanceLogs!);


            modelBuilder.Entity<AttendanceLog>(entity =>
            {
                entity.ToTable("AttendanceLog", tb => tb.HasComment("Журнал посещаемости"));

                entity.Property(e => e.Marker)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.HasOne(d => d.Student).WithMany(p => p.AttendanceLogs)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttendanceLog_Students");
            });

            modelBuilder.Entity<DutySchedule>(entity =>
            {
                entity.ToTable("DutySchedule");

                entity.HasOne(d => d.Student).WithMany(p => p.DutySchedules)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DutySchedule_Students");
            });

            modelBuilder.Entity<EventO>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Organizer).IsRequired();
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });


            modelBuilder.Entity<PurityRaidLog>(entity =>
            {
                entity.Property(e => e.Marker)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Room).WithMany(p => p.PurityRaidLogs)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurityRaidLogs_Rooms");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Gender).HasMaxLength(1);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Patronymic).HasMaxLength(50);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.RepresentativeName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.RepresentativePatronymic).HasMaxLength(100);
                entity.Property(e => e.RepresentativePhone).HasMaxLength(20);
                entity.Property(e => e.RepresentativeSurname)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Group).WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students_Groups");

                entity.HasOne(d => d.Room).WithMany(p => p.Students)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Students_Rooms");

                entity.HasOne(d => d.Status).WithMany(p => p.Students)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Students_Statuses");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(250);
                entity.Property(e => e.Patronymic).HasMaxLength(50);
                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.HasOne(d => d.Role).WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
