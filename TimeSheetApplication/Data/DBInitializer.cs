using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Models;
using TimeSheetApplication.Models.TimeSheetSystem;

namespace TimeSheetApplication.Data
{
    public class DBInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, 
                                      UserManager<ApplicationUser> userManager, 
                                      RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
            
            Debug.WriteLine("In Initializer");

            //// Look for any employees.
            //if (context.Employees.Any())
            //{
            //    Debug.WriteLine("DB has been seeded");
            //    return;   // DB has been seeded
            //}

            if (!context.LaborGrades.Any())
            {
                List<LaborGrade> LaborGrades = new List<LaborGrade>()
                {
                    new LaborGrade {Grade = "P1", PayAmount = 10},
                    new LaborGrade {Grade = "P2", PayAmount = 20},
                    new LaborGrade {Grade = "P3", PayAmount = 30},
                    new LaborGrade {Grade = "P4", PayAmount = 40},
                    new LaborGrade {Grade = "P5", PayAmount = 50},
                    new LaborGrade {Grade = "P6", PayAmount = 60},
                    new LaborGrade {Grade = "D1", PayAmount = 70},
                    new LaborGrade {Grade = "D2", PayAmount = 80},
                };

                context.LaborGrades.AddRange(LaborGrades);
                context.SaveChanges();
            }

            if (!context.Employees.Any())
            {
                List<Employee> Employees = new List<Employee>()
                {
                    new Employee {EmployeeNumber = "1000001", FirstName = "Wyatt", LastName = "Ariss", Grade = "P1", EmployeeIntials = "WAA"},
                    new Employee {EmployeeNumber = "1000002", FirstName = "Nathaniel", LastName = "Thomas", Grade = "P2", EmployeeIntials = "MNT"},
                    new Employee {EmployeeNumber = "1000003", FirstName = "Chloee", LastName = "Robertson", Grade = "P2", EmployeeIntials = "CLR"},
                    new Employee {EmployeeNumber = "1000004", FirstName = "Harvard", LastName = "Sung", Grade = "P2", EmployeeIntials = "HS"},
                    new Employee {EmployeeNumber = "1000005", FirstName = "John", LastName = "Park", Grade = "P2", EmployeeIntials = "JP"},
                    new Employee {EmployeeNumber = "1000006", FirstName = "Shely", LastName = "Lin", Grade = "P2", EmployeeIntials = "SL"},
                    new Employee {EmployeeNumber = "1000007", FirstName = "Rei", LastName = "Ruiz", Grade = "P2", EmployeeIntials = "RR"},
                    new Employee {EmployeeNumber = "1000008", FirstName = "Raymond", LastName = "Gollinger", Grade = "P2", EmployeeIntials = "RG"},
                    new Employee {EmployeeNumber = "1000009", FirstName = "Victor", LastName = "Starzynski", Grade = "P2", EmployeeIntials = "VS"},
                    new Employee {EmployeeNumber = "1000010", FirstName = "Waylon", LastName = "Ching", Grade = "P2", EmployeeIntials = "WC"},
                    new Employee {EmployeeNumber = "1000011", FirstName = "Kenneth", LastName = "Li", Grade = "P2", EmployeeIntials = "KEN"},
                    new Employee {EmployeeNumber = "1000012", FirstName = "Donald", LastName = "Watson", Grade = "P2", EmployeeIntials = "BDW"},
                };

                context.Employees.AddRange(Employees);
                context.SaveChanges();
            }

            string email = "a@a.a";
            string password = "P@$$w0rd";
            string role = "Administrator";
            string employeeNumber = "1000010";

            if (await userManager.FindByNameAsync(email) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmployeeNumber = employeeNumber
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role);
                }
            }

            if (!context.Projects.Any())
            {
                List<Project> Projects = new List<Project>()
                {
                    new Project {ProjectNumber = "12345", Description = "This is project 12345"},
                    new Project {ProjectNumber = "09876", Description = "This is project 09876"},
                };

                context.Projects.AddRange(Projects);
                context.SaveChanges();
            }

            if (!context.WorkPackages.Any())
            {
                List<WorkPackage> WorkPackages = new List<WorkPackage>()
                {
                    new WorkPackage {ProjectNumber = "12345", WorkPackageNumber = "B0000", Description = "This is project 12345"},
                    new WorkPackage {ProjectNumber = "12345", WorkPackageNumber = "A0000",Description = "This is project 09876"},
                    new WorkPackage {ProjectNumber = "09876", WorkPackageNumber = "A0000", Description = "This is project 12345"},
                    new WorkPackage {ProjectNumber = "09876", WorkPackageNumber = "B0000",Description = "This is project 09876"},
                };

                context.WorkPackages.AddRange(WorkPackages);
                context.SaveChanges();
            }

            if (!context.Timesheets.Any())
            {
                List<Timesheet> Timesheets = new List<Timesheet>()
                {
                    new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09)},
                    new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02)},
                    new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 09)},
                    new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 02)},

                    new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 09)},
                    new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 02)},
                    new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 09)},
                    new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02)},

                    new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 09)},
                    new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 02)},
                    new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09)},
                    new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02)},

                    new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 09)},
                    new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 02)},
                    new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 09)},
                    new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02)},

                    new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 09)},
                    new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02)},
                    new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 09)},
                    new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02)},

                    new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 09)},
                    new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02)},
                    new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09)},
                    new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02)},
                };

                context.Timesheets.AddRange(Timesheets);
                context.SaveChanges();
            }
            
            if (!context.TimesheetRows.Any())
            {
                List<TimesheetRow> TimesheetRows = new List<TimesheetRow>()
                {
                    new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},

                    new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},

                    new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},

                    new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},

                    new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},

                    new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},

                    new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},

                    new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},

                    new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},

                    new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},

                    new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},

                    new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                    new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                };

                context.TimesheetRows.AddRange(TimesheetRows);
                context.SaveChanges();
            }
            
        }
    }
}
