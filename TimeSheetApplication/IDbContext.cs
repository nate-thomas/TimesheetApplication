using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApplication.Models.TimeSheetSystem;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TimeSheetApplication.Interfaces
{
    public interface IDbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<LaborGrade> LaborGrades { get; set; }
        DbSet<ProjectStatus> ProjectStatus { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<TimesheetStatus> TimesheetStatus { get; set; }
        DbSet<TimesheetRow> TimesheetRows { get; set; }
        DbSet<Timesheet> Timesheets { get; set; }
        DbSet<WorkPackage> WorkPackages { get; set; }

        DbSet<ResponsibleEngineerBudget> ResponsibleEngineerBudgets { get; set; }
        DbSet<ProjectTeam> ProjectTeams { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
