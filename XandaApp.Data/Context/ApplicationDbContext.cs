using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XandaApp.Data.Models;

namespace XandaApp.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {

        }

        private static readonly string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + baseDirectory + "\\xandadocs\\database\\xandaDB.db");
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<EmailSettings> EmailSettings { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleItem> ScheduleItems { get; set; }
        public DbSet<SmtpConfig> SmtpConfig { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Device>().ToTable("Device");
            modelBuilder.Entity<EmailSettings>().ToTable("EmailSettings");
            modelBuilder.Entity<Media>().ToTable("Media");
            modelBuilder.Entity<Schedule>().ToTable("Schedule");
            modelBuilder.Entity<ScheduleItem>().ToTable("ScheduleItem");
            modelBuilder.Entity<SmtpConfig>().ToTable("SmtpConfig");
        }

    }
}
