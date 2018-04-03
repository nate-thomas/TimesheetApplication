using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Interfaces;
using TimeSheetApplication.Models;
using TimeSheetApplication.Models.TimeSheetSystem;

namespace TimeSheetApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LaborGrade> LaborGrades { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimesheetStatus> TimesheetStatus { get; set; }
        public DbSet<TimesheetRow> TimesheetRows { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<WorkPackage> WorkPackages { get; set; }

        public DbSet<ResponsibleEngineerBudget> ResponsibleEngineerBudgets { get; set; }
        public DbSet<REBbyGrade> REBbyGrades { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        //public DbSet<WPassignment> WPassignments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<LaborGrade>().ToTable("LaborGrades");

            modelBuilder.Entity<ProjectStatus>().ToTable("ProjectStatus");
            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<TimesheetStatus>().ToTable("TimesheetStatus");


            modelBuilder.Entity<TimesheetRow>().ToTable("TimesheetRows").HasKey(c => new { c.EmployeeNumber, c.EndDate, c.ProjectNumber, c.WorkPackageNumber });
            modelBuilder.Entity<Timesheet>().ToTable("Timesheets").HasKey(c => new { c.EmployeeNumber, c.EndDate });
            modelBuilder.Entity<WorkPackage>().ToTable("WorkPackages").HasKey(c => new { c.ProjectNumber, c.WorkPackageNumber });

            modelBuilder.Entity<ProjectTeam>().ToTable("ProjectTeams").HasKey(c => new { c.EmployeeNumber, c.ProjectNumber });
            modelBuilder.Entity<ResponsibleEngineerBudget>().ToTable("ResponsibleEngineerBudgets").HasKey(c => new { c.ProjectNumber, c.WorkPackageNumber, c.EndDate });
            modelBuilder.Entity<REBbyGrade>().ToTable("REBbyGrades").HasKey(c => new { c.ProjectNumber, c.WorkPackageNumber, c.EndDate, c.Grade });

            //modelBuilder.Entity<WPassignment>().ToTable("WPassignments").HasKey(c => new { c.ProjectNumber, c.WorkPackageNumber, c.EmployeeNumber});
        }
    }
}
