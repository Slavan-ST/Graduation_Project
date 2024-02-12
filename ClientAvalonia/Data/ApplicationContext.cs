using ClientAvalonia.Models;
using HarfBuzzSharp;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ClientAvalonia.Data
{
    public class ApplicationContext : DbContext
    {
        //коннект к БД
        static string _connectionString = @"Server = SlavanST\Slavan; Database = SystemO; User id = sa; Password = 123; TrustServerCertificate = True; ";

        //Таблицы
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .HasPrincipalKey(e => e.Id);
        }
    }
}
