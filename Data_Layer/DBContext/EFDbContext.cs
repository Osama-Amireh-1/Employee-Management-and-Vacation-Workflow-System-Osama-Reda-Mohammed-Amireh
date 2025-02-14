using Data_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.DBContext
{
    public class EFDbContext : DbContext
    {
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Positions> Positions { get; set; }

        public DbSet<RequestStates> RequestStates { get; set; }
        public DbSet<VacationRequests> VacationRequests { get; set; }
        public DbSet<VacationTypes> VacationTypes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=EmployeeManagementAndVacationSystemDB;User id = sa; Password=mnbvcxz@4114; TrustServerCertificate=True;").UseLazyLoadingProxies(); ;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Departments>(e =>
            {
                e.HasKey(k => k.DepartmentId);
                e.Property(k => k.DepartmentId).ValueGeneratedOnAdd().UseIdentityColumn();
                e.Property(k => k.DepartmentName).HasMaxLength(50).IsRequired();

            });
            modelBuilder.Entity<Positions>(e =>
            {
                e.HasKey(k => k.PositionId);
                e.Property(k => k.PositionId).ValueGeneratedOnAdd().UseIdentityColumn();
                e.Property(k => k.PositionName).HasMaxLength(30).IsRequired();
            });
            modelBuilder.Entity<Employees>(e =>
            {
                e.HasKey(k => k.EmployeeNumber);
                e.Property(k => k.EmployeeNumber).HasMaxLength(6).IsRequired();
                e.Property(k => k.EmployeeName).HasMaxLength(20).IsRequired();
                e.HasOne(k => k.Department).WithMany().HasForeignKey(k => k.DepartmentId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(k => k.Position).WithMany().HasForeignKey(k => k.PositionId).OnDelete(DeleteBehavior.Restrict);
                e.Property(k => k.GenderCode).IsRequired().HasMaxLength(1);
                e.HasOne(k => k.ReportedToEmployee).WithMany().HasForeignKey(k => k.ReportedToEmployeeNumber).OnDelete(DeleteBehavior.Restrict);
                e.Property(k => k.ReportedToEmployeeNumber).HasMaxLength(6).IsRequired(false);
                e.Property(k => k.VacationDaysLeft).HasDefaultValue(24);
                e.ToTable("Employees", t => t.HasCheckConstraint("CK_VacationDaysLeft", "[VacationDaysLeft] <= 24"));
                e.Property(k => k.Salary).HasPrecision(18, 2);

            });

            modelBuilder.Entity<VacationTypes>(e =>
            {
                e.HasKey(k => k.VacationTypeCode);
                e.Property(k => k.VacationTypeCode).HasMaxLength(1).IsRequired();
                e.Property(k => k.VacationTypeName).HasMaxLength(20).IsRequired();

            });
            modelBuilder.Entity<RequestStates>(e =>
            {
                e.HasKey(k => k.StateId);
                e.Property(k => k.StateName).HasMaxLength(10).IsRequired();
                e.Property(h => h.StateId).ValueGeneratedNever();
            });
            modelBuilder.Entity<VacationRequests>(e =>
            {
                e.HasKey(k => k.RequestId);
                e.Property(k => k.RequestId).ValueGeneratedOnAdd().UseIdentityColumn();
                e.Property(k => k.RequestubmissionDate).IsRequired();
                e.Property(k => k.RequestubmissionDate).HasDefaultValueSql("GETDATE()");
                e.Property(k => k.Description).IsRequired().HasMaxLength(100);
                e.HasOne(k => k.Employee).WithMany(k => k.Vacations).HasForeignKey(k => k.EmployeeNumber).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(k => k.VacationType).WithMany().HasForeignKey(k => k.VacationTypeCode).OnDelete(DeleteBehavior.Restrict);
                e.Property(k => k.VacationTypeCode).HasMaxLength(1);
                e.Property(k => k.StartDate).IsRequired();
                e.Property(k => k.EndDate).IsRequired();
                e.Property(k => k.TotalVacationDays).IsRequired();
                e.HasOne(k => k.RequestStates).WithMany().HasForeignKey(k => k.RequestStateId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(k => k.ApprovedByEmployee).WithMany().HasForeignKey(k => k.ApprovedByEmployeeNumber).OnDelete(DeleteBehavior.Restrict);
                e.Property(k => k.ApprovedByEmployeeNumber).IsRequired(false);
                e.HasOne(k => k.DeclinedByEmployee).WithMany().HasForeignKey(k => k.DeclinedByEmployeeNumber).OnDelete(DeleteBehavior.Restrict);
                e.Property(k => k.DeclinedByEmployeeNumber).IsRequired(false);

            });

        }
    }
}
