using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Models;
using TimeSheetApplication.Models.TimeSheetSystem;

namespace TimeSheetApplication.Data {
    public class DBInitializer {
        public static async Task Initialize(ApplicationDbContext context,
                                      UserManager<ApplicationUser> userManager,
                                      RoleManager<IdentityRole> roleManager) {
            context.Database.EnsureCreated();

            Debug.WriteLine("In Initializer");

            // Initializing Labor Grades
            if (!context.LaborGrades.Any()) {
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

            // Initializing Employees
            if (!context.Employees.Any()) {
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

            // Initializing Roles
            string role1 = "Administrator";
            if (await roleManager.FindByNameAsync(role1) == null) {
                await roleManager.CreateAsync(new IdentityRole(role1));
            }

            string role2 = "Project Manager";
            if (await roleManager.FindByNameAsync(role2) == null) {
                await roleManager.CreateAsync(new IdentityRole(role2));
            }

            string role3 = "Human Resources";
            if (await roleManager.FindByNameAsync(role3) == null) {
                await roleManager.CreateAsync(new IdentityRole(role3));
            }

            string role4 = "Supervisor";
            if (await roleManager.FindByNameAsync(role4) == null) {
                await roleManager.CreateAsync(new IdentityRole(role4));
            }

            string role5 = "Responsible Engineer";
            if (await roleManager.FindByNameAsync(role5) == null) {
                await roleManager.CreateAsync(new IdentityRole(role5));
            }

            string role6 = "Developer";
            if (await roleManager.FindByNameAsync(role6) == null) {
                await roleManager.CreateAsync(new IdentityRole(role6));
            }

            // Initializing Users
            string password = "P@$$w0rd";

            string emp1 = "1000001"; // Wyatt
            if (await userManager.FindByNameAsync(emp1) == null) {
                var user = new ApplicationUser {
                    UserName = emp1,
                    EmployeeNumber = emp1
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Project Manager");
                }
            }

            string emp2 = "1000002"; // Nate
            if (await userManager.FindByNameAsync(emp2) == null) {
                var user = new ApplicationUser {
                    UserName = emp2,
                    EmployeeNumber = emp2
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }

            string emp3 = "1000003"; // Chloee
            if (await userManager.FindByNameAsync(emp3) == null) {
                var user = new ApplicationUser {
                    UserName = emp3,
                    EmployeeNumber = emp3
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Human Resources");
                }
            }

            string emp4 = "1000004"; // Harvard
            if (await userManager.FindByNameAsync(emp4) == null) {
                var user = new ApplicationUser {
                    UserName = emp4,
                    EmployeeNumber = emp4
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Responsible Engineer");
                }
            }

            string emp5 = "1000005"; // John
            if (await userManager.FindByNameAsync(emp5) == null) {
                var user = new ApplicationUser {
                    UserName = emp5,
                    EmployeeNumber = emp5
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Supervisor");
                }
            }

            string emp6 = "1000006"; // Shely
            if (await userManager.FindByNameAsync(emp6) == null) {
                var user = new ApplicationUser {
                    UserName = emp6,
                    EmployeeNumber = emp6
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Developer");
                }
            }

            string emp7 = "1000007"; // Rei
            if (await userManager.FindByNameAsync(emp7) == null) {
                var user = new ApplicationUser {
                    UserName = emp7,
                    EmployeeNumber = emp7
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Developer");
                }
            }

            string emp8 = "1000008"; // Raymond
            if (await userManager.FindByNameAsync(emp8) == null) {
                var user = new ApplicationUser {
                    UserName = emp8,
                    EmployeeNumber = emp8
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Developer");
                }
            }

            string emp9 = "1000009"; // Victor
            if (await userManager.FindByNameAsync(emp9) == null) {
                var user = new ApplicationUser {
                    UserName = emp9,
                    EmployeeNumber = emp9
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Developer");
                }
            }

            string emp10 = "1000010"; // Waylon
            if (await userManager.FindByNameAsync(emp10) == null) {
                var user = new ApplicationUser {
                    UserName = emp10,
                    EmployeeNumber = emp10
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Developer");
                }
            }

            string emp11 = "1000011"; // Kenneth
            if (await userManager.FindByNameAsync(emp11) == null) {
                var user = new ApplicationUser {
                    UserName = emp11,
                    EmployeeNumber = emp11
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Developer");
                }
            }

            string emp12 = "1000012"; // Donald
            if (await userManager.FindByNameAsync(emp12) == null) {
                var user = new ApplicationUser {
                    UserName = emp12,
                    EmployeeNumber = emp12
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Developer");
                }
            }

            // Initializing ProjectStatus
            if (!context.ProjectStatus.Any()) {
                List<ProjectStatus> ProjectStatus = new List<ProjectStatus>()
                {
                    new ProjectStatus {StatusName="Current", Description = "A currently running Project"},
                    new ProjectStatus {StatusName="Archived", Description = "An Archived Project"},
                };

                context.ProjectStatus.AddRange(ProjectStatus);
                context.SaveChanges();
            }

            // Initializing Projects
            if (!context.Projects.Any()) {
                List<Project> Projects = new List<Project>()
                {
                    new Project {ProjectNumber = "12345", Description = "This is project 12345"},
                    new Project {ProjectNumber = "09876", Description = "This is project 09876"},
                };

                context.Projects.AddRange(Projects);
                context.SaveChanges();
            }

            // Initializing Work Packages
            if (!context.WorkPackages.Any()) {
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


            // Initializing TimesheetStatus
            if (!context.TimesheetStatus.Any()) {
                List<TimesheetStatus> TimesheetStatus = new List<TimesheetStatus>()
                {
                    new TimesheetStatus {StatusName="Draft", Description = "A draft of a timesheet. This time sheet has not been submitted yet."},
                    new TimesheetStatus {StatusName="Submitted", Description = "A Timesheet that has been submitted but not approved/reject by the supervisor"},
                    new TimesheetStatus {StatusName="Approved", Description = "An Approved timesheet."},
                    new TimesheetStatus {StatusName="Rejected", Description = "A rejected timesheet."},
                };

                context.TimesheetStatus.AddRange(TimesheetStatus);
                context.SaveChanges();
            }

            if (!context.Timesheets.Any()) {
                List<Timesheet> Timesheets = new List<Timesheet>()
                {
                    new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09), StatusName = "Draft"},
                    new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), StatusName = "Submitted"},
                    new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                    new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 02), StatusName = "Rejected"},

                    new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 09), StatusName = "Draft"},
                    new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 02), StatusName = "Submitted"},
                    new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                    new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02), StatusName = "Rejected"},

                    new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 09), StatusName = "Draft"},
                    new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 02), StatusName = "Submitted"},
                    new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                    new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02), StatusName = "Rejected"},

                    new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 09), StatusName = "Draft"},
                    new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 02), StatusName = "Submitted"},
                    new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                    new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02), StatusName = "Rejected"},

                    new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 09), StatusName = "Draft"},
                    new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02), StatusName = "Submitted"},
                    new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                    new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02), StatusName = "Rejected"},

                    new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 09), StatusName = "Draft"},
                    new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02), StatusName = "Submitted"},
                    new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                    new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02), StatusName = "Rejected"},
                };

                context.Timesheets.AddRange(Timesheets);
                context.SaveChanges();
            }

            // Initializing Timesheet Rows
            if (!context.TimesheetRows.Any()) {
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



            // Initializing Project Teams
            if (!context.ProjectTeams.Any()) {
                List<ProjectTeam> ProjectTeams = new List<ProjectTeam>()
                {
                    new ProjectTeam {ProjectNumber = "12345", EmployeeNumber = "1000001"},
                    new ProjectTeam {ProjectNumber = "12345", EmployeeNumber = "1000002"},
                    new ProjectTeam {ProjectNumber = "12345", EmployeeNumber = "1000003"},

                    new ProjectTeam {ProjectNumber = "09876", EmployeeNumber = "1000001"},
                    new ProjectTeam {ProjectNumber = "09876", EmployeeNumber = "1000004"},
                    new ProjectTeam {ProjectNumber = "09876", EmployeeNumber = "1000005"},
                };

                context.ProjectTeams.AddRange(ProjectTeams);
                context.SaveChanges();
            }

            // Initializing ResponsibleEngineerBudget
            if (!context.ResponsibleEngineerBudgets.Any()) {
                List<ResponsibleEngineerBudget> ResponsibleEngineerBudgets = new List<ResponsibleEngineerBudget>()
                {
                    new ResponsibleEngineerBudget {ProjectNumber = "12345", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 02)},
                    new ResponsibleEngineerBudget {ProjectNumber = "12345", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 09)},
                    new ResponsibleEngineerBudget {ProjectNumber = "12345", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 16)},

                    new ResponsibleEngineerBudget {ProjectNumber = "09876", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 02)},
                    new ResponsibleEngineerBudget {ProjectNumber = "09876", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 09)},
                    new ResponsibleEngineerBudget {ProjectNumber = "09876", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 16)},
                };

                context.ResponsibleEngineerBudgets.AddRange(ResponsibleEngineerBudgets);
                context.SaveChanges();
            }

        }
    }
}
