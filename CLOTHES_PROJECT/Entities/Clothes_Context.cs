using CLOTHES_PROJECT.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.Arm;

namespace CLOTHES_PROJECT.Entities
{
    public class Clothes_Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; DataBase = Clothes_Company; Trusted_Connection = True; Encrypt = False");
            
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(Emp =>
            {
                Emp.Property(E => E.Bdate).HasColumnType("Date");
            });
            modelBuilder.Entity<Department>(Dep => 
            {
                Dep.Property(D => D.MGRStartDate).HasColumnType("Date");
            });
            modelBuilder.Entity<WORKS_ON>(EmpProject =>
            {
                EmpProject.HasKey(W => new {W.ESSN,W.PNO});
            });
            modelBuilder.Entity<Dependent>(Dpn => 
            {
                Dpn.HasKey(D => new {D.ESSN,D.Dependent_Name });
                Dpn.Property(D => D.Bdate).HasColumnType("Date");
            });
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Dependent> Dependents { get; set; }
        public virtual DbSet<WORKS_ON> WORKS_ON { get; set; }


    }
}
