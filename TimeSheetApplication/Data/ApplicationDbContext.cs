using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Models.TimeSheetSystem;

namespace TimeSheetApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            
        }

        public DbSet<AuthorizationCodes> AuthorizationCodes { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<LaborGrades> LaborGrades { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<TimesheetRows> TimesheetRows { get; set; }
        public DbSet<Timesheets> Timesheets { get; set; }
        public DbSet<WorkPackages> WorkPackages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<AuthorizationCodes>().ToTable("AuthorizationCodes");
            modelBuilder.Entity<Employees>().ToTable("Employees");
            modelBuilder.Entity<LaborGrades>().ToTable("LaborGrades");
            modelBuilder.Entity<Projects>().ToTable("Projects");
            modelBuilder.Entity<TimesheetRows>().ToTable("TimesheetRows").HasKey(c => new { c.EmployeeNumber, c.EndDate, c.ProjectNumber, c.WorkPackageNumber });
/*            modelBuilder.Entity<TimesheetRows>()
                .HasOne(p => p.WorkPackage)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BlogForeignKey);*/
            modelBuilder.Entity<Timesheets>().ToTable("Timesheets").HasKey(c => new { c.EmployeeNumber, c.EndDate});
            modelBuilder.Entity<WorkPackages>().ToTable("WorkPackages").HasKey(c => new {c.ProjectNumber, c.WorkPackageNumber });
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<AuthorizationCodes>().ToTable("AuthorizationCodes");
            modelBuilder.Entity<Employees>().ToTable("Employees");
            modelBuilder.Entity<LaborGrades>().ToTable("LaborGrades");
            modelBuilder.Entity<Projects>().ToTable("Projects");
            modelBuilder.Entity<TimesheetRows>().ToTable("TimesheetRows");
            modelBuilder.Entity<Timesheets>().ToTable("Timesheets");
            modelBuilder.Entity<WorkPackages>().ToTable("WorkPackages");
        }*/
    }
}
