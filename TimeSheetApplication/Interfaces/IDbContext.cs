﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Models;
using TimeSheetApplication.Models.TimeSheetSystem;

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
    }
}
