using System;
using Microsoft.EntityFrameworkCore;
using psw_ftn.Models;
using psw_ftn.Models.User;
using psw_ftn.Models.User.UserTypes;

namespace psw_ftn.Data
{
   public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasIndex(u => u.Email)
            .IsUnique();

            
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<CheckUp>().ToTable("CheckUps");
            modelBuilder.Entity<CancelledCheckUp>().ToTable("CancelledCheckUps"); 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<CheckUp> CheckUps { get; set; }
        public DbSet<HistoryCheckUp> HistoryCheckUps { get; set; }
        public DbSet<CancelledCheckUp> CancelledCheckUps { get; set; }
    }
}