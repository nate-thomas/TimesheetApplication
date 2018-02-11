using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Models.TimeSheetSystem;

namespace TimeSheetApplication.Data
{
    public class DBInitializer
    {
        public static void Initialize(ApplicationDbContext context) {
            context.Database.EnsureCreated();


            Debug.WriteLine("In Initializer");
            List<AuthorizationCodes> AuthorizationCodes = new List<AuthorizationCodes>() {
                new AuthorizationCodes {AuthCode = "HumanResources"},
                new AuthorizationCodes {AuthCode = "SuperAdmin"},
                new AuthorizationCodes {AuthCode = "Developer"},
            };

            context.AuthorizationCodes.AddRange(AuthorizationCodes);
            context.SaveChanges();

            List<LaborGrades> LaborGrades = new List<LaborGrades>() {
                new LaborGrades {Grade = "P1", PayAmount = 10},
                new LaborGrades {Grade = "P2", PayAmount = 20},
                new LaborGrades {Grade = "P3", PayAmount = 30},
                new LaborGrades {Grade = "P4", PayAmount = 40},
                new LaborGrades {Grade = "P5", PayAmount = 50},
                new LaborGrades {Grade = "P6", PayAmount = 60},
                new LaborGrades {Grade = "D1", PayAmount = 70},
                new LaborGrades {Grade = "D2", PayAmount = 80},
            };

            context.LaborGrades.AddRange(LaborGrades);
            context.SaveChanges();

            List<Employees> Employees = new List<Employees>() {
                new Employees {EmployeeNumber = "1000001", FirstName = "Wyatt", LastName = "Ariss", Grade = "P1", EmployeeIntials = "WAA", AuthCode = "SuperAdmin"},
                new Employees {EmployeeNumber = "1000002", FirstName = "Nathaniel", LastName = "Thomas", Grade = "P2", EmployeeIntials = "MNT", AuthCode = "HumanResources"},
                new Employees {EmployeeNumber = "1000003", FirstName = "Chloee", LastName = "Robertson", Grade = "P2", EmployeeIntials = "CLR", AuthCode = "HumanResources"},
                new Employees {EmployeeNumber = "1000004", FirstName = "Harvard", LastName = "Sung", Grade = "P2", EmployeeIntials = "HS", AuthCode = "HumanResources"},
                new Employees {EmployeeNumber = "1000005", FirstName = "John", LastName = "Park", Grade = "P2", EmployeeIntials = "JP", AuthCode = "HumanResources"},
                new Employees {EmployeeNumber = "1000006", FirstName = "Shely", LastName = "Lin", Grade = "P2", EmployeeIntials = "SL", AuthCode = "HumanResources"},
                new Employees {EmployeeNumber = "1000007", FirstName = "Rei", LastName = "Ruiz", Grade = "P2", EmployeeIntials = "RR", AuthCode = "HumanResources"},
                new Employees {EmployeeNumber = "1000008", FirstName = "Raymond", LastName = "Gollinger", Grade = "P2", EmployeeIntials = "RG", AuthCode = "HumanResources"},
                new Employees {EmployeeNumber = "1000009", FirstName = "Victor", LastName = "Starzynski", Grade = "P2", EmployeeIntials = "VS", AuthCode = "HumanResources"},
                new Employees {EmployeeNumber = "1000010", FirstName = "Waylon", LastName = "Ching", Grade = "P2", EmployeeIntials = "WC", AuthCode = "HumanResources"},
                new Employees {EmployeeNumber = "1000011", FirstName = "Kenneth", LastName = "Li", Grade = "P2", EmployeeIntials = "KEN", AuthCode = "HumanResources"},
                new Employees {EmployeeNumber = "1000012", FirstName = "Donald", LastName = "Watson", Grade = "P2", EmployeeIntials = "BDW", AuthCode = "HumanResources"},
            };

            context.Employees.AddRange(Employees);
            context.SaveChanges();

            List<Projects> Projects = new List<Projects>() {
                new Projects {ProjectNumber = "12345", Description = "This is project 12345"},
                new Projects {ProjectNumber = "09876", Description = "This is project 09876"},
            };

            context.Projects.AddRange(Projects);
            context.SaveChanges();

            List<WorkPackages> WorkPackages = new List<WorkPackages>() {
                new WorkPackages {ProjectNumber = "12345", WorkPackageNumber = "B0000", Description = "This is project 12345"},
                new WorkPackages {ProjectNumber = "12345", WorkPackageNumber = "A0000",Description = "This is project 09876"},
                new WorkPackages {ProjectNumber = "09876", WorkPackageNumber = "A0000", Description = "This is project 12345"},
                new WorkPackages {ProjectNumber = "09876", WorkPackageNumber = "B0000",Description = "This is project 09876"},
            };

            context.WorkPackages.AddRange(WorkPackages);
            context.SaveChanges();

            List<Timesheets> Timesheets = new List<Timesheets>() {
                new Timesheets {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09)},
                new Timesheets {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02)},
                new Timesheets {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 09)},
                new Timesheets {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 02)},

                new Timesheets {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 09)},
                new Timesheets {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 02)},
                new Timesheets {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 09)},
                new Timesheets {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02)},

                new Timesheets {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 09)},
                new Timesheets {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 02)},
                new Timesheets {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09)},
                new Timesheets {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02)},

                new Timesheets {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 09)},
                new Timesheets {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 02)},
                new Timesheets {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 09)},
                new Timesheets {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02)},

                new Timesheets {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 09)},
                new Timesheets {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02)},
                new Timesheets {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 09)},
                new Timesheets {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02)},

                new Timesheets {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 09)},
                new Timesheets {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02)},
                new Timesheets {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09)},
                new Timesheets {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02)},
            };

            context.Timesheets.AddRange(Timesheets);
            context.SaveChanges();


            List<TimesheetRows> TimesheetRows = new List<TimesheetRows>() {
                new TimesheetRows {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},

                new TimesheetRows {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},

                new TimesheetRows {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},

                new TimesheetRows {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},

                new TimesheetRows {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},

                new TimesheetRows {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000006", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},

                new TimesheetRows {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},

                new TimesheetRows {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000007", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000008", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},

                new TimesheetRows {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},

                new TimesheetRows {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000010", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},

                new TimesheetRows {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "12345", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "12345", WorkPackageNumber = "B0000"},

                new TimesheetRows {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 09), ProjectNumber = "09876", WorkPackageNumber = "B0000"},
                new TimesheetRows {EmployeeNumber = "1000012", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "09876", WorkPackageNumber = "B0000"},




            };

            context.TimesheetRows.AddRange(TimesheetRows);
            context.SaveChanges();
        }
    }
}
