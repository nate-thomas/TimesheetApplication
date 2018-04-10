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
                    new Employee {EmployeeNumber = "1000001", FirstName = "Wyatt", LastName = "Ariss", Grade = "P5", EmployeeIntials = "WAA"},
                    new Employee {EmployeeNumber = "1000002", FirstName = "Nathaniel", LastName = "Thomas", Grade = "P2", EmployeeIntials = "MNT", SupervisorNumber = "1000001"},
                    new Employee {EmployeeNumber = "1000003", FirstName = "Chloee", LastName = "Robertson", Grade = "P2", EmployeeIntials = "CLR", SupervisorNumber = "1000001"},
                    new Employee {EmployeeNumber = "1000004", FirstName = "Harvard", LastName = "Sung", Grade = "P2", EmployeeIntials = "HS", SupervisorNumber = "1000001"},
                    new Employee {EmployeeNumber = "1000005", FirstName = "John", LastName = "Park", Grade = "P2", EmployeeIntials = "JP"},
                    new Employee {EmployeeNumber = "1000006", FirstName = "Shely", LastName = "Lin", Grade = "P2", EmployeeIntials = "SL", SupervisorNumber = "1000005"},
                    new Employee {EmployeeNumber = "1000007", FirstName = "Rei", LastName = "Ruiz", Grade = "P2", EmployeeIntials = "RR", SupervisorNumber = "1000005"},
                    new Employee {EmployeeNumber = "1000008", FirstName = "Raymond", LastName = "Gollinger", Grade = "P2", EmployeeIntials = "RG", SupervisorNumber = "1000005"},
                    new Employee {EmployeeNumber = "1000009", FirstName = "Victor", LastName = "Starzynski", Grade = "P2", EmployeeIntials = "VS", SupervisorNumber = "1000005"},
                    new Employee {EmployeeNumber = "1000010", FirstName = "Waylon", LastName = "Ching", Grade = "P2", EmployeeIntials = "WC", SupervisorNumber = "1000001"},
                    new Employee {EmployeeNumber = "1000011", FirstName = "Kenneth", LastName = "Li", Grade = "P2", EmployeeIntials = "KEN", SupervisorNumber = "1000001"},
                    new Employee {EmployeeNumber = "1000012", FirstName = "Donald", LastName = "Watson", Grade = "P2", EmployeeIntials = "BDW", SupervisorNumber = "1000001"},
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
                    await userManager.AddToRoleAsync(user, "Responsible Engineer");
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
                    new Project {ProjectNumber = "WebPrj128", Description = "[Current] Web project for BC Hydro (Prj02)", StatusName="Current", Budget = 15000},
                    new Project {ProjectNumber = "WebPrj098", Description = "[Current] Web project for Burnaby Publc Library", StatusName="Current", Budget = 13000},
                    new Project {ProjectNumber = "WebPrj2018", Description = "[Archieved] Web project for Oranj Fitness", StatusName="Archived", Budget = 50000},
                    new Project {ProjectNumber = "WebPrj127", Description = "[Current] Web project for BC Hydro (Prj01)", StatusName="Current", Budget = 75000},
                    new Project {ProjectNumber = "SW789", Description = "[Current] Software project for Lululemon", StatusName="Current", Budget = 5000},
                    new Project {ProjectNumber = "SW8985", Description = "[Archived] Software project for Dairy Farmers of Canada", StatusName="Archived", Budget = 22000},
                    new Project {ProjectNumber = "SW999", Description = "[Current] Software project for The Vancouver Sun", StatusName="Current", Budget = 88000},
                    new Project {ProjectNumber = "SW090", Description = "[Current] Software project for Merck", StatusName="Current", Budget = 30000},
                    new Project {ProjectNumber = "Cloud001", Description = "[Current] Cloud Computing project for BCAA", StatusName="Current", Budget = 150000},
                };

                context.Projects.AddRange(Projects);
                context.SaveChanges();
            }

            // Initializing Work Packages
            if (!context.WorkPackages.Any()) {
                List<WorkPackage> WorkPackages = getWorkPackageData(); //Method located at the bottom of this class

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
                List<Timesheet> Timesheets = getTimesheets();

                context.Timesheets.AddRange(Timesheets);
                context.SaveChanges();
            }

            // Initializing Timesheet Rows
            if (!context.TimesheetRows.Any()) {
                List<TimesheetRow> TimesheetRows = getTimesheetRows();

                context.TimesheetRows.AddRange(TimesheetRows);
                context.SaveChanges();
            }



             // Initializing Project Teams
             if (!context.ProjectTeams.Any()) {
                 List<ProjectTeam> ProjectTeams = getTeams();

                 context.ProjectTeams.AddRange(ProjectTeams);
                 context.SaveChanges();
             }

            // Initializing ResponsibleEngineerBudget
            if (!context.ResponsibleEngineerBudgets.Any()) {
                 List<ResponsibleEngineerBudget> ResponsibleEngineerBudgets = new List<ResponsibleEngineerBudget>()
                 {
                     new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "A2", EndDate = new DateTime(2018, 03, 30) },
                     new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "A4", EndDate = new DateTime(2018, 03, 30)},
                     new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "AA", EndDate = new DateTime(2018, 03, 30) },
                     new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "A11", EndDate = new DateTime(2018, 03, 30)},
                     new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "A13", EndDate = new DateTime(2018, 03, 30)},
                     new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "A125", EndDate = new DateTime(2018, 03, 30)},
                     new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "B6", EndDate = new DateTime(2018, 03, 30)},
                     new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "BAA", EndDate = new DateTime(2018, 03, 30)},
                     new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "BA6", EndDate = new DateTime(2018, 03, 30)},
                     new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "BA7", EndDate = new DateTime(2018, 03, 30)},
                 };

                 context.ResponsibleEngineerBudgets.AddRange(ResponsibleEngineerBudgets);
                 context.SaveChanges();
             }

            
             // Initializing REBbyGrade
             if (!context.REBbyGrades.Any()) {
                 List<REBbyGrade> REBbyGrades = new List<REBbyGrade>()
                 {
                     /*new REBbyGrade {ProjectNumber = "12345", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 02), Grade = "P1"},
                     new REBbyGrade {ProjectNumber = "12345", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 09), Grade = "P1"},
                     new REBbyGrade {ProjectNumber = "12345", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 16), Grade = "P1"},

                     new REBbyGrade {ProjectNumber = "12345", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 02), Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "12345", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 09), Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "12345", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 16), Grade = "P2"},



                     new REBbyGrade {ProjectNumber = "09876", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 02), Grade = "P1"},
                     new REBbyGrade {ProjectNumber = "09876", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 09), Grade = "P1"},
                     new REBbyGrade {ProjectNumber = "09876", WorkPackageNumber = "A0000", EndDate = new DateTime(2018, 02, 16), Grade = "P1"},

                     new REBbyGrade {ProjectNumber = "09876", WorkPackageNumber = "B0000", EndDate = new DateTime(2018, 02, 02), Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "09876", WorkPackageNumber = "B0000", EndDate = new DateTime(2018, 02, 09), Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "09876", WorkPackageNumber = "B0000", EndDate = new DateTime(2018, 02, 16), Grade = "P2"},*/

                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "A2",   EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 20, Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "A4",   EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 30, Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "AA",   EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 50, Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "A11",  EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 70, Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "A13",  EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 20, Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "A125", EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 40, Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "B6",   EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 15, Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "BAA",  EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 6, Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "BA6",  EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 11, Grade = "P2"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "BA7",  EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 2, Grade = "P2"},

                    new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "A2",   EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 80, Grade = "P5"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "A4",   EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 50, Grade = "P5"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "AA",   EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 9, Grade = "P5"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "A11",  EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 15, Grade = "P5"},
                     /**/new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "A13",  EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 8, Grade = "P1"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "A125", EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 25, Grade = "P1"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "B6",   EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 40, Grade = "P1"},
                     /**/new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "BAA",  EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 5, Grade = "P1"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "BA6",  EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 1, Grade = "P1"},
                     new REBbyGrade {ProjectNumber = "WebPrj128", WorkPackageNumber = "BA7",  EndDate = new DateTime(2018, 03, 30),EstimatedManHours = 7, Grade = "P1"},
                 };

                 context.REBbyGrades.AddRange(REBbyGrades);
                 context.SaveChanges();
             }



            // Initializing Work Packages assignments for the teams
            if (!context.WPassignments.Any()) {
                 List<WPassignment> WPassignments = getAssignments();

                 context.WPassignments.AddRange(WPassignments);
                 context.SaveChanges();
             }

        }

        private static List<WorkPackage> getWorkPackageData() {
            List<WorkPackage> WorkPackages = new List<WorkPackage>() {
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "A", Description = "Project#:WebPrj128 WorkingPackage#:A", Budget=6000},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "A1", Description = "Project#:WebPrj128 WorkingPackage#:A1", Budget=1500},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "A2", Description = "Project#:WebPrj128 WorkingPackage#:A2", Budget=1500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "A4", Description = "Project#:WebPrj128 WorkingPackage#:A4", Budget=750,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "AA", Description = "Project#:WebPrj128 WorkingPackage#:AA", Budget=2250,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "A12", Description = "Project#:WebPrj128 WorkingPackage#:A12", Budget=1050},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "A11", Description = "Project#:WebPrj128 WorkingPackage#:A11", Budget=300,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "A13", Description = "Project#:WebPrj128 WorkingPackage#:A13", Budget=150,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "A125", Description = "Project#:WebPrj128 WorkingPackage#:A125", Budget=600,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "B", Description = "Project#:WebPrj128 WorkingPackage#:B", Budget=9000},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "B3", Description = "Project#:WebPrj128 WorkingPackage#:B3", Budget=3750},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "B6", Description = "Project#:WebPrj128 WorkingPackage#:B6", Budget=2250,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "BA", Description = "Project#:WebPrj128 WorkingPackage#:BA", Budget=3000},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "BAA", Description = "Project#:WebPrj128 WorkingPackage#:BAA", Budget=1500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "BA6", Description = "Project#:WebPrj128 WorkingPackage#:BA6", Budget=900,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "BA7", Description = "Project#:WebPrj128 WorkingPackage#:BA7", Budget=300,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "B33", Description = "Project#:WebPrj128 WorkingPackage#:B33", Budget=1500},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "B36", Description = "Project#:WebPrj128 WorkingPackage#:B36", Budget=1500,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "B333", Description = "Project#:WebPrj128 WorkingPackage#:B333", Budget=450,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "A", Description = "Project#:WebPrj098 WorkingPackage#:A", Budget=5200},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "A1", Description = "Project#:WebPrj098 WorkingPackage#:A1", Budget=1300},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "A2", Description = "Project#:WebPrj098 WorkingPackage#:A2", Budget=1300,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "A4", Description = "Project#:WebPrj098 WorkingPackage#:A4", Budget=650,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "AA", Description = "Project#:WebPrj098 WorkingPackage#:AA", Budget=1950,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "A12", Description = "Project#:WebPrj098 WorkingPackage#:A12", Budget=910},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "A11", Description = "Project#:WebPrj098 WorkingPackage#:A11", Budget=260,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "A13", Description = "Project#:WebPrj098 WorkingPackage#:A13", Budget=130,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "A125", Description = "Project#:WebPrj098 WorkingPackage#:A125", Budget=520,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "B", Description = "Project#:WebPrj098 WorkingPackage#:B", Budget=7800},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "B3", Description = "Project#:WebPrj098 WorkingPackage#:B3", Budget=3250},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "B6", Description = "Project#:WebPrj098 WorkingPackage#:B6", Budget=1950,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "BA", Description = "Project#:WebPrj098 WorkingPackage#:BA", Budget=2600},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "BAA", Description = "Project#:WebPrj098 WorkingPackage#:BAA", Budget=1300,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "BA6", Description = "Project#:WebPrj098 WorkingPackage#:BA6", Budget=780,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "BA7", Description = "Project#:WebPrj098 WorkingPackage#:BA7", Budget=260,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "B33", Description = "Project#:WebPrj098 WorkingPackage#:B33", Budget=1300},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "B36", Description = "Project#:WebPrj098 WorkingPackage#:B36", Budget=1300,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj098", WorkPackageNumber = "B333", Description = "Project#:WebPrj098 WorkingPackage#:B333", Budget=390,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "A", Description = "Project#:WebPrj2018 WorkingPackage#:A", Budget=20000},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "A1", Description = "Project#:WebPrj2018 WorkingPackage#:A1", Budget=5000},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "A2", Description = "Project#:WebPrj2018 WorkingPackage#:A2", Budget=5000,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "A4", Description = "Project#:WebPrj2018 WorkingPackage#:A4", Budget=2500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "AA", Description = "Project#:WebPrj2018 WorkingPackage#:AA", Budget=7500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "A12", Description = "Project#:WebPrj2018 WorkingPackage#:A12", Budget=3500},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "A11", Description = "Project#:WebPrj2018 WorkingPackage#:A11", Budget=1000,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "A13", Description = "Project#:WebPrj2018 WorkingPackage#:A13", Budget=500,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "A125", Description = "Project#:WebPrj2018 WorkingPackage#:A125", Budget=2000,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "B", Description = "Project#:WebPrj2018 WorkingPackage#:B", Budget=30000},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "B3", Description = "Project#:WebPrj2018 WorkingPackage#:B3", Budget=12500},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "B6", Description = "Project#:WebPrj2018 WorkingPackage#:B6", Budget=7500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "BA", Description = "Project#:WebPrj2018 WorkingPackage#:BA", Budget=10000},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "BAA", Description = "Project#:WebPrj2018 WorkingPackage#:BAA", Budget=5000,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "BA6", Description = "Project#:WebPrj2018 WorkingPackage#:BA6", Budget=3000,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "BA7", Description = "Project#:WebPrj2018 WorkingPackage#:BA7", Budget=1000,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "B33", Description = "Project#:WebPrj2018 WorkingPackage#:B33", Budget=5000},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "B36", Description = "Project#:WebPrj2018 WorkingPackage#:B36", Budget=5000,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj2018", WorkPackageNumber = "B333", Description = "Project#:WebPrj2018 WorkingPackage#:B333", Budget=1500,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "A", Description = "Project#:WebPrj127 WorkingPackage#:A", Budget=30000},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "A1", Description = "Project#:WebPrj127 WorkingPackage#:A1", Budget=7500},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "A2", Description = "Project#:WebPrj127 WorkingPackage#:A2", Budget=7500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "A4", Description = "Project#:WebPrj127 WorkingPackage#:A4", Budget=3750,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "AA", Description = "Project#:WebPrj127 WorkingPackage#:AA", Budget=11250,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "A12", Description = "Project#:WebPrj127 WorkingPackage#:A12", Budget=5250},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "A11", Description = "Project#:WebPrj127 WorkingPackage#:A11", Budget=1500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "A13", Description = "Project#:WebPrj127 WorkingPackage#:A13", Budget=750,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "A125", Description = "Project#:WebPrj127 WorkingPackage#:A125", Budget=3000,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "B", Description = "Project#:WebPrj127 WorkingPackage#:B", Budget=45000},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "B3", Description = "Project#:WebPrj127 WorkingPackage#:B3", Budget=18750},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "B6", Description = "Project#:WebPrj127 WorkingPackage#:B6", Budget=11250,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "BA", Description = "Project#:WebPrj127 WorkingPackage#:BA", Budget=15000},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "BAA", Description = "Project#:WebPrj127 WorkingPackage#:BAA", Budget=7500,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "BA6", Description = "Project#:WebPrj127 WorkingPackage#:BA6", Budget=4500,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "BA7", Description = "Project#:WebPrj127 WorkingPackage#:BA7", Budget=1500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "B33", Description = "Project#:WebPrj127 WorkingPackage#:B33", Budget=7500},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "B36", Description = "Project#:WebPrj127 WorkingPackage#:B36", Budget=7500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "WebPrj127", WorkPackageNumber = "B333", Description = "Project#:WebPrj127 WorkingPackage#:B333", Budget=2250,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "A", Description = "Project#:SW789 WorkingPackage#:A", Budget=2000},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "A1", Description = "Project#:SW789 WorkingPackage#:A1", Budget=500},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "A2", Description = "Project#:SW789 WorkingPackage#:A2", Budget=500,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "A4", Description = "Project#:SW789 WorkingPackage#:A4", Budget=250,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "AA", Description = "Project#:SW789 WorkingPackage#:AA", Budget=750,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "A12", Description = "Project#:SW789 WorkingPackage#:A12", Budget=350},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "A11", Description = "Project#:SW789 WorkingPackage#:A11", Budget=100,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "A13", Description = "Project#:SW789 WorkingPackage#:A13", Budget=50,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "A125", Description = "Project#:SW789 WorkingPackage#:A125", Budget=200,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "B", Description = "Project#:SW789 WorkingPackage#:B", Budget=3000},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "B3", Description = "Project#:SW789 WorkingPackage#:B3", Budget=1250},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "B6", Description = "Project#:SW789 WorkingPackage#:B6", Budget=750,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "BA", Description = "Project#:SW789 WorkingPackage#:BA", Budget=1000},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "BAA", Description = "Project#:SW789 WorkingPackage#:BAA", Budget=500,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "BA6", Description = "Project#:SW789 WorkingPackage#:BA6", Budget=300,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "BA7", Description = "Project#:SW789 WorkingPackage#:BA7", Budget=100,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "B33", Description = "Project#:SW789 WorkingPackage#:B33", Budget=500},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "B36", Description = "Project#:SW789 WorkingPackage#:B36", Budget=500,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW789", WorkPackageNumber = "B333", Description = "Project#:SW789 WorkingPackage#:B333", Budget=150,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "A", Description = "Project#:SW8985 WorkingPackage#:A", Budget=8800},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "A1", Description = "Project#:SW8985 WorkingPackage#:A1", Budget=2200},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "A2", Description = "Project#:SW8985 WorkingPackage#:A2", Budget=2200,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "A4", Description = "Project#:SW8985 WorkingPackage#:A4", Budget=1100,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "AA", Description = "Project#:SW8985 WorkingPackage#:AA", Budget=3300,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "A12", Description = "Project#:SW8985 WorkingPackage#:A12", Budget=1540},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "A11", Description = "Project#:SW8985 WorkingPackage#:A11", Budget=440,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "A13", Description = "Project#:SW8985 WorkingPackage#:A13", Budget=220,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "A125", Description = "Project#:SW8985 WorkingPackage#:A125", Budget=880,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "B", Description = "Project#:SW8985 WorkingPackage#:B", Budget=13200},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "B3", Description = "Project#:SW8985 WorkingPackage#:B3", Budget=5500},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "B6", Description = "Project#:SW8985 WorkingPackage#:B6", Budget=3300,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "BA", Description = "Project#:SW8985 WorkingPackage#:BA", Budget=4400},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "BAA", Description = "Project#:SW8985 WorkingPackage#:BAA", Budget=2200,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "BA6", Description = "Project#:SW8985 WorkingPackage#:BA6", Budget=1320,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "BA7", Description = "Project#:SW8985 WorkingPackage#:BA7", Budget=440,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "B33", Description = "Project#:SW8985 WorkingPackage#:B33", Budget=2200},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "B36", Description = "Project#:SW8985 WorkingPackage#:B36", Budget=2200,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW8985", WorkPackageNumber = "B333", Description = "Project#:SW8985 WorkingPackage#:B333", Budget=660,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "A", Description = "Project#:SW999 WorkingPackage#:A", Budget=35200},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "A1", Description = "Project#:SW999 WorkingPackage#:A1", Budget=8800},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "A2", Description = "Project#:SW999 WorkingPackage#:A2", Budget=8800,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "A4", Description = "Project#:SW999 WorkingPackage#:A4", Budget=4400,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "AA", Description = "Project#:SW999 WorkingPackage#:AA", Budget=13200,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "A12", Description = "Project#:SW999 WorkingPackage#:A12", Budget=6160},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "A11", Description = "Project#:SW999 WorkingPackage#:A11", Budget=1760,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "A13", Description = "Project#:SW999 WorkingPackage#:A13", Budget=880,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "A125", Description = "Project#:SW999 WorkingPackage#:A125", Budget=3520,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "B", Description = "Project#:SW999 WorkingPackage#:B", Budget=52800},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "B3", Description = "Project#:SW999 WorkingPackage#:B3", Budget=22000},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "B6", Description = "Project#:SW999 WorkingPackage#:B6", Budget=13200,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "BA", Description = "Project#:SW999 WorkingPackage#:BA", Budget=17600},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "BAA", Description = "Project#:SW999 WorkingPackage#:BAA", Budget=8800,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "BA6", Description = "Project#:SW999 WorkingPackage#:BA6", Budget=5280,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "BA7", Description = "Project#:SW999 WorkingPackage#:BA7", Budget=1760,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "B33", Description = "Project#:SW999 WorkingPackage#:B33", Budget=8800},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "B36", Description = "Project#:SW999 WorkingPackage#:B36", Budget=8800,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW999", WorkPackageNumber = "B333", Description = "Project#:SW999 WorkingPackage#:B333", Budget=2640,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "A", Description = "Project#:SW090 WorkingPackage#:A", Budget=12000},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "A1", Description = "Project#:SW090 WorkingPackage#:A1", Budget=3000},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "A2", Description = "Project#:SW090 WorkingPackage#:A2", Budget=3000,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "A4", Description = "Project#:SW090 WorkingPackage#:A4", Budget=1500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "AA", Description = "Project#:SW090 WorkingPackage#:AA", Budget=4500,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "A12", Description = "Project#:SW090 WorkingPackage#:A12", Budget=2100},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "A11", Description = "Project#:SW090 WorkingPackage#:A11", Budget=600,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "A13", Description = "Project#:SW090 WorkingPackage#:A13", Budget=300,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "A125", Description = "Project#:SW090 WorkingPackage#:A125", Budget=1200,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "B", Description = "Project#:SW090 WorkingPackage#:B", Budget=18000},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "B3", Description = "Project#:SW090 WorkingPackage#:B3", Budget=7500},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "B6", Description = "Project#:SW090 WorkingPackage#:B6", Budget=4500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "BA", Description = "Project#:SW090 WorkingPackage#:BA", Budget=6000},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "BAA", Description = "Project#:SW090 WorkingPackage#:BAA", Budget=3000,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "BA6", Description = "Project#:SW090 WorkingPackage#:BA6", Budget=1800,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "BA7", Description = "Project#:SW090 WorkingPackage#:BA7", Budget=600,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "B33", Description = "Project#:SW090 WorkingPackage#:B33", Budget=3000},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "B36", Description = "Project#:SW090 WorkingPackage#:B36", Budget=3000,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "SW090", WorkPackageNumber = "B333", Description = "Project#:SW090 WorkingPackage#:B333", Budget=900,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "A", Description = "Project#:Cloud001 WorkingPackage#:A", Budget=60000},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "A1", Description = "Project#:Cloud001 WorkingPackage#:A1", Budget=15000},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "A2", Description = "Project#:Cloud001 WorkingPackage#:A2", Budget=15000,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "A4", Description = "Project#:Cloud001 WorkingPackage#:A4", Budget=7500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "AA", Description = "Project#:Cloud001 WorkingPackage#:AA", Budget=22500,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "A12", Description = "Project#:Cloud001 WorkingPackage#:A12", Budget=10500},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "A11", Description = "Project#:Cloud001 WorkingPackage#:A11", Budget=3000,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "A13", Description = "Project#:Cloud001 WorkingPackage#:A13", Budget=1500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "A125", Description = "Project#:Cloud001 WorkingPackage#:A125", Budget=6000,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "B", Description = "Project#:Cloud001 WorkingPackage#:B", Budget=90000},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "B3", Description = "Project#:Cloud001 WorkingPackage#:B3", Budget=37500},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "B6", Description = "Project#:Cloud001 WorkingPackage#:B6", Budget=22500,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "BA", Description = "Project#:Cloud001 WorkingPackage#:BA", Budget=30000},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "BAA", Description = "Project#:Cloud001 WorkingPackage#:BAA", Budget=15000,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "BA6", Description = "Project#:Cloud001 WorkingPackage#:BA6", Budget=9000,ResponsibleEngineerNumber = "1000009"},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "BA7", Description = "Project#:Cloud001 WorkingPackage#:BA7", Budget=3000,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "B33", Description = "Project#:Cloud001 WorkingPackage#:B33", Budget=15000},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "B36", Description = "Project#:Cloud001 WorkingPackage#:B36", Budget=15000,ResponsibleEngineerNumber = "1000004"},
                new WorkPackage {ProjectNumber = "Cloud001", WorkPackageNumber = "B333", Description = "Project#:Cloud001 WorkingPackage#:B333", Budget=4500,ResponsibleEngineerNumber = "1000009"},

            };
            return WorkPackages;
        }

        private static List<Timesheet> getTimesheets() {
            List<Timesheet> Timesheets = new List<Timesheet>() {
                new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 23), StatusName = "Submitted"},
                new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 30), StatusName = "Draft"},
                new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 23), StatusName = "Rejected"},
                new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 30), StatusName = "Draft"},
                new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 23), StatusName = "Submitted"},
                new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 30), StatusName = "Draft"},
                new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 23), StatusName = "Rejected"},
                new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 30), StatusName = "Draft"},
                new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 23), StatusName = "Submitted"},
                new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 30), StatusName = "Draft"},
                new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 23), StatusName = "Rejected"},
                new Timesheet {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 30), StatusName = "Draft"},
                new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 23), StatusName = "Rejected"},
                new Timesheet {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 30), StatusName = "Draft"},
                new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 23), StatusName = "Rejected"},
                new Timesheet {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 30), StatusName = "Draft"},
                new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 30), StatusName = "Draft"},
                new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 03, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 03, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 03, 23), StatusName = "Submitted"},
                new Timesheet {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 03, 30), StatusName = "Draft"},
                new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 23), StatusName = "Rejected"},
                new Timesheet {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 30), StatusName = "Draft"},
                new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 09), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 16), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 23), StatusName = "Approved"},
                new Timesheet {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 30), StatusName = "Draft"},

            };
            return Timesheets;
        }

        private static List<TimesheetRow> getTimesheetRows() {
            List<TimesheetRow> t = new List<TimesheetRow>() {
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW090", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 3,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW8985", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 0,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW999", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 3,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW8985", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 2,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj128", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 2,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW090", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 2,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW8985", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 1,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW789", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 2,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW789", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 2,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj128", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 1,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW999", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 1,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "Cloud001", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 2,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj098", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 2,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW090", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 2,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW8985", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 2,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj098", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 0,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj128", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 2,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "SW999", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 1,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "SW789", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 0,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "SW090", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 0,Thursday = 3,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW999", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 0,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW999", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 0,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "WebPrj098", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 2,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW8985", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 2,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW789", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 2,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "WebPrj127", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 3,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "WebPrj098", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 1,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "Cloud001", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 2,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj128", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 1,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW999", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 2,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj098", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 2,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW090", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 2,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "Cloud001", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 3,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW090", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 1,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW8985", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 0,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW090", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 2,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj128", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 1,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW8985", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 2,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "SW999", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 1,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj127", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 1,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj2018", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 3,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "WebPrj127", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 3,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "WebPrj128", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 3,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "Cloud001", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 0,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "WebPrj098", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 0,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW999", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 0,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW999", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 1,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj098", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 3,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW789", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 0,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj2018", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 3,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 3,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW8985", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 0,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW789", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 0,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW090", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 0,Thursday = 3,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "WebPrj127", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 2,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW789", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 2,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj098", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 3,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW090", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 3,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "Cloud001", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 0,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 0,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 0,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "Cloud001", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 3,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "WebPrj127", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 1,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "WebPrj098", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 1,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW999", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 3,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW090", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 0,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "WebPrj2018", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 1,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "WebPrj128", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 2,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "Cloud001", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 2,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj2018", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 0,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW999", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 2,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj2018", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 2,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj098", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 3,Thursday = 3,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj127", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 1,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj127", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 0,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj098", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 2,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj098", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 2,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW789", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 0,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj128", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 3,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW8985", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 2,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW789", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 1,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW789", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 0,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "Cloud001", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 2,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW789", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 2,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "Cloud001", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 3,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj2018", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 0,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW8985", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 0,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj127", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 0,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj128", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 2,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj127", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 2,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj127", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 1,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "SW789", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 2,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "SW8985", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 0,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "SW8985", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 1,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "SW999", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 3,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "WebPrj128", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 2,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW090", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 3,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW090", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 0,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW999", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 3,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "Cloud001", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 2,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW090", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 3,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj127", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 1,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj098", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 2,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj127", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 3,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW789", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 2,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj128", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 2,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW8985", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 2,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW999", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 3,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW090", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 2,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj127", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 0,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj127", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 2,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "WebPrj127", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 1,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW999", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 3,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW090", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 2,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "Cloud001", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 3,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "Cloud001", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 0,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW789", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 3,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW789", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 1,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW999", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 2,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW789", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 3,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "Cloud001", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 3,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW8985", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 3,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 1,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj2018", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 1,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW8985", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 1,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "Cloud001", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 2,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj127", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 3,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "Cloud001", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 0,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj128", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 1,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW999", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 1,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj098", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 1,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 2,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW999", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 3,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW789", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 1,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW8985", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 2,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW999", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 0,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 3,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW999", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 1,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW999", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 1,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "SW8985", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 1,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "WebPrj128", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 2,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW8985", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 2,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW789", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 2,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "Cloud001", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 2,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "WebPrj098", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 0,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW999", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 0,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "Cloud001", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 3,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "Cloud001", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 0,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW8985", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 3,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj127", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 3,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj128", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 2,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW8985", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 0,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW789", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 1,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW090", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 2,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW789", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 2,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "WebPrj128", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 2,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW999", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 3,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 3,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "WebPrj098", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 1,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW789", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 0,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW789", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 1,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj2018", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 1,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj128", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 2,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 1,Thursday = 3,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj128", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 3,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj128", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 3,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "WebPrj127", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 1,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "WebPrj098", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 1,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW789", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 1,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW789", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 3,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW8985", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 1,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "WebPrj128", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 1,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 3,Thursday = 3,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "Cloud001", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 2,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW999", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 3,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj2018", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 0,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW999", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 0,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW090", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 0,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj2018", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 0,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj128", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 3,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj2018", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 0,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW8985", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 0,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj098", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 3,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "Cloud001", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 3,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW789", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 3,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW8985", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 0,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW789", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 0,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "WebPrj098", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 2,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW8985", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 1,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj2018", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 0,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "SW090", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 3,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "SW090", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 0,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "SW8985", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 1,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "SW999", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 3,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "WebPrj098", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 2,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "Cloud001", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 3,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW090", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 1,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "WebPrj2018", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 0,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "Cloud001", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 3,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW090", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 2,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "WebPrj128", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 2,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "WebPrj098", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 3,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW999", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 1,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW789", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 3,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW789", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 3,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW090", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 1,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW999", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 2,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj127", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 1,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW999", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 2,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj2018", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 0,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 3,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 0,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj127", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 0,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW8985", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 1,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW090", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 0,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 0,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj098", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 2,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "Cloud001", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 2,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj127", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 0,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "SW999", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 3,Wednesday = 0,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "SW090", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 0,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "SW090", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 0,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "SW999", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 3,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "SW999", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 1,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "Cloud001", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 3,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW090", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 2,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW789", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 3,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "WebPrj128", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 2,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW999", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 0,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "Cloud001", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 3,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW090", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 0,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW8985", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 1,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW090", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 0,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW789", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 2,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW090", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 2,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "Cloud001", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 0,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "Cloud001", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 0,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW999", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 3,Thursday = 3,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW999", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 2,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj128", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 1,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj128", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 2,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "WebPrj098", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 2,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "WebPrj128", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 0,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW090", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 2,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj128", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 3,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj2018", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 0,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "Cloud001", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 3,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "WebPrj127", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 1,Thursday = 3,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW090", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 2,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW090", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 0,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "Cloud001", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 3,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW8985", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 1,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW789", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 3,Thursday = 3,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW8985", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 1,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW090", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 2,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW999", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 1,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "Cloud001", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 2,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj128", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 3,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 1,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW090", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 0,Wednesday = 1,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "Cloud001", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 2,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "WebPrj2018", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 2,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW789", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 2,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "WebPrj2018", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 0,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "WebPrj127", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 0,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW789", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 1,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW789", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 1,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj098", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 2,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj128", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 0,Thursday = 1,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj2018", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 2,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj128", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 2,Thursday = 3,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "WebPrj127", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 3,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "WebPrj127", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 1,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "Cloud001", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 2,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "SW8985", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 2,Wednesday = 3,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "WebPrj098", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 1,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "WebPrj098", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 3,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "WebPrj098", WorkPackageNumber = "A4",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 2,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "Cloud001", WorkPackageNumber = "BA6",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 0,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW8985", WorkPackageNumber = "BAA",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 0,Thursday = 3,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW090", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 1,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW789", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 0,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW999", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 0,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW090", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 0,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "WebPrj098", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 2,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW789", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 0,Thursday = 1,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW789", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 2,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "WebPrj127", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 1,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW789", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 0,Wednesday = 3,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "SW090", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 2,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj127", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 3,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj2018", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 1,Wednesday = 0,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "SW090", WorkPackageNumber = "A2",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 2,Thursday = 3,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj098", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 3,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 16), ProjectNumber = "WebPrj128", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 3,Thursday = 2,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW999", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 3,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "SW8985", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 1,Thursday = 0,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 23), ProjectNumber = "WebPrj098", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 0,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "Cloud001", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 1,Thursday = 0,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "WebPrj2018", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 2,Wednesday = 0,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW090", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 1,Wednesday = 3,Thursday = 2,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW999", WorkPackageNumber = "B6",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 3,Thursday = 1,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 02), ProjectNumber = "SW789", WorkPackageNumber = "B36",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 3,Thursday = 2,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 09), ProjectNumber = "SW090", WorkPackageNumber = "B333",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 3,Thursday = 3,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "SW789", WorkPackageNumber = "A11",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 0,Wednesday = 1,Thursday = 2,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "SW8985", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 0,Thursday = 0,Friday = 0,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "WebPrj2018", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 0,Wednesday = 1,Thursday = 3,Friday = 1,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 16), ProjectNumber = "SW090", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 3,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW090", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 1,Wednesday = 3,Thursday = 0,Friday = 3,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 23), ProjectNumber = "SW8985", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 2,Tuesday = 3,Wednesday = 3,Thursday = 1,Friday = 2,},
                new TimesheetRow {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 03, 30), ProjectNumber = "SW090", WorkPackageNumber = "A13",Saturday = 0,Sunday = 0,Monday = 1,Tuesday = 3,Wednesday = 0,Thursday = 1,Friday = 1,},

            };
            return t;
        }

        private static List<ProjectTeam> getTeams() {
            List<ProjectTeam> t = new List<ProjectTeam>() {
                new ProjectTeam {ProjectNumber = "SW090", EmployeeNumber = "1000001"},
                new ProjectTeam {ProjectNumber = "SW8985", EmployeeNumber = "1000001"},
                new ProjectTeam {ProjectNumber = "SW999", EmployeeNumber = "1000001"},
                new ProjectTeam {ProjectNumber = "WebPrj128", EmployeeNumber = "1000001"},
                new ProjectTeam {ProjectNumber = "SW789", EmployeeNumber = "1000001"},
                new ProjectTeam {ProjectNumber = "Cloud001", EmployeeNumber = "1000001"},
                new ProjectTeam {ProjectNumber = "WebPrj098", EmployeeNumber = "1000001"},
                new ProjectTeam {ProjectNumber = "WebPrj127", EmployeeNumber = "1000001"},
                new ProjectTeam {ProjectNumber = "Cloud001", EmployeeNumber = "1000002"},
                new ProjectTeam {ProjectNumber = "WebPrj128", EmployeeNumber = "1000002"},
                new ProjectTeam {ProjectNumber = "SW999", EmployeeNumber = "1000002"},
                new ProjectTeam {ProjectNumber = "WebPrj098", EmployeeNumber = "1000002"},
                new ProjectTeam {ProjectNumber = "SW090", EmployeeNumber = "1000002"},
                new ProjectTeam {ProjectNumber = "SW8985", EmployeeNumber = "1000002"},
                new ProjectTeam {ProjectNumber = "WebPrj127", EmployeeNumber = "1000002"},
                new ProjectTeam {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000002"},
                new ProjectTeam {ProjectNumber = "SW999", EmployeeNumber = "1000003"},
                new ProjectTeam {ProjectNumber = "WebPrj098", EmployeeNumber = "1000003"},
                new ProjectTeam {ProjectNumber = "SW789", EmployeeNumber = "1000003"},
                new ProjectTeam {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000003"},
                new ProjectTeam {ProjectNumber = "SW8985", EmployeeNumber = "1000003"},
                new ProjectTeam {ProjectNumber = "SW090", EmployeeNumber = "1000003"},
                new ProjectTeam {ProjectNumber = "WebPrj127", EmployeeNumber = "1000003"},
                new ProjectTeam {ProjectNumber = "Cloud001", EmployeeNumber = "1000003"},
                new ProjectTeam {ProjectNumber = "WebPrj128", EmployeeNumber = "1000003"},
                new ProjectTeam {ProjectNumber = "Cloud001", EmployeeNumber = "1000004"},
                new ProjectTeam {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000004"},
                new ProjectTeam {ProjectNumber = "SW999", EmployeeNumber = "1000004"},
                new ProjectTeam {ProjectNumber = "WebPrj098", EmployeeNumber = "1000004"},
                new ProjectTeam {ProjectNumber = "WebPrj127", EmployeeNumber = "1000004"},
                new ProjectTeam {ProjectNumber = "SW789", EmployeeNumber = "1000004"},
                new ProjectTeam {ProjectNumber = "WebPrj128", EmployeeNumber = "1000004"},
                new ProjectTeam {ProjectNumber = "SW8985", EmployeeNumber = "1000004"},
                new ProjectTeam {ProjectNumber = "SW090", EmployeeNumber = "1000004"},
                new ProjectTeam {ProjectNumber = "WebPrj127", EmployeeNumber = "1000005"},
                new ProjectTeam {ProjectNumber = "WebPrj098", EmployeeNumber = "1000005"},
                new ProjectTeam {ProjectNumber = "SW789", EmployeeNumber = "1000005"},
                new ProjectTeam {ProjectNumber = "WebPrj128", EmployeeNumber = "1000005"},
                new ProjectTeam {ProjectNumber = "SW8985", EmployeeNumber = "1000005"},
                new ProjectTeam {ProjectNumber = "SW999", EmployeeNumber = "1000005"},
                new ProjectTeam {ProjectNumber = "SW090", EmployeeNumber = "1000005"},
                new ProjectTeam {ProjectNumber = "Cloud001", EmployeeNumber = "1000005"},
                new ProjectTeam {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000006"},
                new ProjectTeam {ProjectNumber = "SW8985", EmployeeNumber = "1000006"},
                new ProjectTeam {ProjectNumber = "Cloud001", EmployeeNumber = "1000006"},
                new ProjectTeam {ProjectNumber = "WebPrj127", EmployeeNumber = "1000006"},
                new ProjectTeam {ProjectNumber = "WebPrj128", EmployeeNumber = "1000006"},
                new ProjectTeam {ProjectNumber = "SW999", EmployeeNumber = "1000006"},
                new ProjectTeam {ProjectNumber = "WebPrj098", EmployeeNumber = "1000006"},
                new ProjectTeam {ProjectNumber = "SW789", EmployeeNumber = "1000006"},
                new ProjectTeam {ProjectNumber = "SW8985", EmployeeNumber = "1000007"},
                new ProjectTeam {ProjectNumber = "WebPrj127", EmployeeNumber = "1000007"},
                new ProjectTeam {ProjectNumber = "WebPrj128", EmployeeNumber = "1000007"},
                new ProjectTeam {ProjectNumber = "SW789", EmployeeNumber = "1000007"},
                new ProjectTeam {ProjectNumber = "SW090", EmployeeNumber = "1000007"},
                new ProjectTeam {ProjectNumber = "SW999", EmployeeNumber = "1000007"},
                new ProjectTeam {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000007"},
                new ProjectTeam {ProjectNumber = "WebPrj098", EmployeeNumber = "1000007"},
                new ProjectTeam {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000008"},
                new ProjectTeam {ProjectNumber = "Cloud001", EmployeeNumber = "1000008"},
                new ProjectTeam {ProjectNumber = "SW999", EmployeeNumber = "1000008"},
                new ProjectTeam {ProjectNumber = "SW090", EmployeeNumber = "1000008"},
                new ProjectTeam {ProjectNumber = "WebPrj128", EmployeeNumber = "1000008"},
                new ProjectTeam {ProjectNumber = "SW8985", EmployeeNumber = "1000008"},
                new ProjectTeam {ProjectNumber = "WebPrj098", EmployeeNumber = "1000008"},
                new ProjectTeam {ProjectNumber = "SW789", EmployeeNumber = "1000008"},
                new ProjectTeam {ProjectNumber = "SW789", EmployeeNumber = "1000009"},
                new ProjectTeam {ProjectNumber = "SW090", EmployeeNumber = "1000009"},
                new ProjectTeam {ProjectNumber = "SW999", EmployeeNumber = "1000009"},
                new ProjectTeam {ProjectNumber = "WebPrj127", EmployeeNumber = "1000009"},
                new ProjectTeam {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000009"},
                new ProjectTeam {ProjectNumber = "SW8985", EmployeeNumber = "1000009"},
                new ProjectTeam {ProjectNumber = "WebPrj098", EmployeeNumber = "1000009"},
                new ProjectTeam {ProjectNumber = "Cloud001", EmployeeNumber = "1000009"},
                new ProjectTeam {ProjectNumber = "WebPrj128", EmployeeNumber = "1000009"},
                new ProjectTeam {ProjectNumber = "SW999", EmployeeNumber = "1000010"},
                new ProjectTeam {ProjectNumber = "Cloud001", EmployeeNumber = "1000010"},
                new ProjectTeam {ProjectNumber = "SW090", EmployeeNumber = "1000010"},
                new ProjectTeam {ProjectNumber = "SW8985", EmployeeNumber = "1000010"},
                new ProjectTeam {ProjectNumber = "SW789", EmployeeNumber = "1000010"},
                new ProjectTeam {ProjectNumber = "WebPrj128", EmployeeNumber = "1000010"},
                new ProjectTeam {ProjectNumber = "WebPrj098", EmployeeNumber = "1000010"},
                new ProjectTeam {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000010"},
                new ProjectTeam {ProjectNumber = "WebPrj127", EmployeeNumber = "1000010"},
                new ProjectTeam {ProjectNumber = "Cloud001", EmployeeNumber = "1000011"},
                new ProjectTeam {ProjectNumber = "SW8985", EmployeeNumber = "1000011"},
                new ProjectTeam {ProjectNumber = "SW789", EmployeeNumber = "1000011"},
                new ProjectTeam {ProjectNumber = "SW090", EmployeeNumber = "1000011"},
                new ProjectTeam {ProjectNumber = "SW999", EmployeeNumber = "1000011"},
                new ProjectTeam {ProjectNumber = "WebPrj128", EmployeeNumber = "1000011"},
                new ProjectTeam {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000011"},
                new ProjectTeam {ProjectNumber = "WebPrj127", EmployeeNumber = "1000011"},
                new ProjectTeam {ProjectNumber = "WebPrj098", EmployeeNumber = "1000011"},
                new ProjectTeam {ProjectNumber = "SW090", EmployeeNumber = "1000012"},
                new ProjectTeam {ProjectNumber = "SW789", EmployeeNumber = "1000012"},
                new ProjectTeam {ProjectNumber = "SW999", EmployeeNumber = "1000012"},
                new ProjectTeam {ProjectNumber = "WebPrj098", EmployeeNumber = "1000012"},
                new ProjectTeam {ProjectNumber = "WebPrj127", EmployeeNumber = "1000012"},
                new ProjectTeam {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000012"},
                new ProjectTeam {ProjectNumber = "WebPrj128", EmployeeNumber = "1000012"},
                new ProjectTeam {ProjectNumber = "SW8985", EmployeeNumber = "1000012"},
                new ProjectTeam {ProjectNumber = "Cloud001", EmployeeNumber = "1000012"},

            };
            return t;
        }

        private static List<WPassignment> getAssignments() {
            List<WPassignment> a = new List<WPassignment>() {
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000001", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000001", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000001", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000001", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000001", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000001", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000001", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000001", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000001", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000001", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000001", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000001", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000001", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000001", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000001", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000001", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000001", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000001", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000001", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000001", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000001", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000001", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000001", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000001", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000002", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000002", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000002", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000002", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000002", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000002", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000002", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000002", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000002", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000002", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000002", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000002", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000002", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000002", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000002", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000002", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000002", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000003", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000003", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000003", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000003", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000003", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000003", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000003", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000003", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000003", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000003", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000003", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000003", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000003", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000003", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000003", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000003", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000003", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000003", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000003", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000003", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000003", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000004", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000004", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000004", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000004", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000004", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000004", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000004", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000004", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000004", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000004", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000004", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000004", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000004", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000004", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000004", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000004", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000004", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000004", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000004", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000004", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000004", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000004", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000004", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000004", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000004", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000004", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000004", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000004", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000004", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000004", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000004", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000005", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000005", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000005", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000005", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000005", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000005", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000005", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000005", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000005", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000005", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000005", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000005", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000005", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000005", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000005", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000005", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000005", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000005", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000005", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000005", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000005", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000006", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000006", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000006", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000006", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000006", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000006", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000006", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000006", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000006", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000006", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000006", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000006", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000006", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000006", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000006", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000006", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000006", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000006", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000006", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000006", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000006", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000006", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000006", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000007", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000007", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000007", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000007", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000007", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000007", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000007", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000007", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000007", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000007", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000007", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000007", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000007", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000007", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000007", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000007", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000007", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000007", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000007", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000007", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000007", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000008", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000008", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000008", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000008", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000008", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000008", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000008", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000008", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000008", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000008", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000008", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000008", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000008", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000008", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000008", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000008", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000008", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000008", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000008", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000008", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000008", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000008", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000008", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000008", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000008", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000008", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000008", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000008", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000008", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000008", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000008", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000008", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000009", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000009", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000009", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000009", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000009", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000009", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000009", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000009", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000009", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000009", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000009", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000009", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000009", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000009", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000009", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000009", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000009", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000009", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000009", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000009", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000009", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000009", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000010", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000010", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000010", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000010", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000010", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000010", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000010", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000010", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000010", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000010", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000010", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000010", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000010", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000010", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000010", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000010", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000010", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000010", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000010", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000010", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000011", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000011", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000011", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000011", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000011", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000011", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000011", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000011", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000011", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000011", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000011", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000011", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000011", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000011", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000011", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000011", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000011", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000011", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000011", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000011", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000011", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000011", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000011", WorkPackageNumber = "A4"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000011", WorkPackageNumber = "BA6"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000011", WorkPackageNumber = "BAA"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000012", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000012", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000012", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000012", WorkPackageNumber = "A2"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000012", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000012", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000012", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000012", WorkPackageNumber = "A125"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000012", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "WebPrj127", EmployeeNumber = "1000012", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000012", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000012", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "WebPrj128", EmployeeNumber = "1000012", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000012", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000012", WorkPackageNumber = "AA"},
                new WPassignment {ProjectNumber = "WebPrj098", EmployeeNumber = "1000012", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "Cloud001", EmployeeNumber = "1000012", WorkPackageNumber = "BA7"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000012", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000012", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "SW999", EmployeeNumber = "1000012", WorkPackageNumber = "B6"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000012", WorkPackageNumber = "B36"},
                new WPassignment {ProjectNumber = "SW090", EmployeeNumber = "1000012", WorkPackageNumber = "B333"},
                new WPassignment {ProjectNumber = "SW789", EmployeeNumber = "1000012", WorkPackageNumber = "A11"},
                new WPassignment {ProjectNumber = "SW8985", EmployeeNumber = "1000012", WorkPackageNumber = "A13"},
                new WPassignment {ProjectNumber = "WebPrj2018", EmployeeNumber = "1000012", WorkPackageNumber = "AA"},

            };
            return a;
        }
    }
}
