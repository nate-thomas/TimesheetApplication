using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheetApplication.ApiControllers;
using TimeSheetApplication.Interfaces;
using TimeSheetApplication.Models.TimeSheetSystem;
using Xunit;

namespace XUnitTestProject
{
    public class TimesheetsApiControllerTests
    {
        [Fact]
        public async Task GetTimesheetsByEmployeeNumber_WhenModelStateIsInvalid()
        {
            var controller = new TimesheetsApiController(null);
            controller.ModelState.AddModelError("key", "message");

            var result = await controller.GetTimesheetsByEmployeeNumber(null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task GetTimesheetsByEmployeeNumber_TimesheetsNotFound()
        {
            string empNumber = "1234567";
            var dbContext = new Mock<IDbContext>();
            var mockList = Helpers.ToAsyncDbSetMock(testTimesheets);
            dbContext.Setup(x => x.Timesheets).Returns(mockList.Object);

            var controller = new TimesheetsApiController(dbContext.Object);

            var result = await controller.GetTimesheetsByEmployeeNumber(empNumber);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetTimesheetsByEmployeeNumber_Successful()
        {
            string empNumber = "1000001";
            var dbContext = new Mock<IDbContext>();
            var mockList = Helpers.ToAsyncDbSetMock(testTimesheets);
            dbContext.Setup(x => x.Timesheets).Returns(mockList.Object);

            var controller = new TimesheetsApiController(dbContext.Object);

            var result = await controller.GetTimesheetsByEmployeeNumber(empNumber);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetTimesheetByEmployeeNumberAndEndDate_WhenModelStateIsInvalid()
        {
            DateTime date = new DateTime(2018, 03, 30);
            var controller = new TimesheetsApiController(null);
            controller.ModelState.AddModelError("key", "message");

            var result = await controller.GetTimesheetByEmployeeNumberAndEndDate(null, date);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task GetTimesheetByEmployeeNumberAndEndDate_WhenEndDateIsNotFriday()
        {
            DateTime date = new DateTime(2018, 03, 29);
            string expected = "InsertOrUpdateTimesheet: end date is not a Friday";
            var controller = new TimesheetsApiController(null);

            var result = await controller.GetTimesheetByEmployeeNumberAndEndDate(null, date);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact]
        public async Task GetTimesheetByEmployeeNumberAndEndDate_WhenTimesheetsNotFound()
        {
            string empNumber = "1000009";
            DateTime endDate = new DateTime(2018, 02, 23);
            var dbContext = new Mock<IDbContext>();
            var mockList = Helpers.ToAsyncDbSetMock(testTimesheets);
            dbContext.Setup(x => x.Timesheets).Returns(mockList.Object);

            var controller = new TimesheetsApiController(dbContext.Object);

            var result = await controller.GetTimesheetByEmployeeNumberAndEndDate(empNumber, endDate);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetTimesheetByEmployeeNumberAndEndDate_Successful()
        {
            string empNumber = "1000004";
            DateTime endDate = new DateTime(2018, 02, 23);
            var dbContext = new Mock<IDbContext>();
            var mockList = Helpers.ToAsyncDbSetMock(testTimesheets);
            dbContext.Setup(x => x.Timesheets).Returns(mockList.Object);

            var controller = new TimesheetsApiController(dbContext.Object);

            var result = await controller.GetTimesheetByEmployeeNumberAndEndDate(empNumber, endDate);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetSupervisorTimesheetsByEmployeeNumber_WhenModelStateIsInvalid()
        {
            var controller = new TimesheetsApiController(null);
            controller.ModelState.AddModelError("key", "message");

            var result = await controller.GetSupervisorTimesheetsByEmployeeNumber(null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task GetSupervisorTimesheetsByEmployeeNumber_WhenTimesheetsNotFound()
        {
            string empNumber = "1000006";
            var dbContext = new Mock<IDbContext>();
            var mockListTimesheets = MockDbSet(testTimesheets);
            var mockListEmployees = MockDbSet(testEmployees);
            dbContext.Setup(x => x.Timesheets).Returns(mockListTimesheets.Object);
            dbContext.Setup(x => x.Employees).Returns(mockListEmployees.Object);

            var controller = new TimesheetsApiController(dbContext.Object);

            var result = await controller.GetSupervisorTimesheetsByEmployeeNumber(empNumber);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetSupervisorTimesheetsByEmployeeNumber_Successful()
        {
            string empNumber = "1000001";
            var dbContext = new Mock<IDbContext>();
            var mockListTimesheets = MockDbSet(testTimesheets);
            var mockListEmployees = MockDbSet(testEmployees);
            dbContext.Setup(x => x.Timesheets).Returns(mockListTimesheets.Object);
            dbContext.Setup(x => x.Employees).Returns(mockListEmployees.Object);

            var controller = new TimesheetsApiController(dbContext.Object);

            var result = await controller.GetSupervisorTimesheetsByEmployeeNumber(empNumber);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InsertOrUpdateTimesheet_WhenModelStateIsInvalid()
        {
            DateTime endDate = new DateTime(2018, 02, 23);
            var controller = new TimesheetsApiController(null);
            controller.ModelState.AddModelError("key", "message");

            var result = await controller.InsertOrUpdateTimesheet(null, endDate, null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task InsertOrUpdateTimesheet_WhenEndDateIsNotFriday()
        {
            DateTime endDate = new DateTime(2018, 03, 29);
            string expected = "InsertOrUpdateTimesheet: end date is not a Friday";
            var controller = new TimesheetsApiController(null);

            var result = await controller.InsertOrUpdateTimesheet(null, endDate, null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact]
        public void InsertOrUpdateTimesheet_WhenEmployeeNumberDoesNotMatchTimesheetEmployeeNumber()
        {
            DateTime endDate = new DateTime(2018, 03, 30);
            string empNumber = "1000001";
            Timesheet timesheet = new Timesheet { EmployeeNumber = "1000002" };
            string expected = "InsertOrUpdateTimesheet: inconsistent timesheet employee number and/or end date";
            var controller = new TimesheetsApiController(null);

            var result = controller.InsertOrUpdateTimesheet(empNumber, endDate, timesheet);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact]
        public void InsertOrUpdateTimesheet_WhenEndDateDoesNotMatchTimesheetEndDate()
        {
            DateTime endDate = new DateTime(2018, 03, 30);
            string empNumber = "1000001";
            Timesheet timesheet = new Timesheet { EmployeeNumber = "1000001" , EndDate = new DateTime(2018, 03, 23) };
            string expected = "InsertOrUpdateTimesheet: inconsistent timesheet employee number and/or end date";
            var controller = new TimesheetsApiController(null);

            var result = controller.InsertOrUpdateTimesheet(empNumber, endDate, timesheet);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact]
        public async Task InsertOrUpdateTimesheet_WhenNoExistingTimesheetFound()
        {
            string empNumber = "1000009";
            DateTime endDate = new DateTime(2018, 02, 02);
            Timesheet timesheet = new Timesheet { EmployeeNumber = "1000009", EndDate = new DateTime(2018, 02, 02) };
            var dbContext = new Mock<IDbContext>();
            var mockListTimesheets = Helpers.ToAsyncDbSetMock(testTimesheets);
            dbContext.Setup(x => x.Timesheets).Returns(mockListTimesheets.Object);
            var controller = new TimesheetsApiController(dbContext.Object);

            var result = await controller.InsertOrUpdateTimesheet(empNumber, endDate, timesheet);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact(Skip = "Cannot mock dbContext.Entry()")]
        public async Task InsertOrUpdateTimesheet_WhenExistingTimesheetFound()
        {
            string expected = "InsertOrUpdateTimesheet: inconsistent timesheet.timesheetRows - employee number and/or end date";
            string empNumber = "1000001";
            DateTime endDate = new DateTime(2018, 02, 02);
            Timesheet timesheet = new Timesheet { EmployeeNumber = "1000011", EndDate = new DateTime(2018, 02, 02) };
            var dbContext = new Mock<IDbContext>();
            var mockListTimesheets = Helpers.ToAsyncDbSetMock(testTimesheets);
            dbContext.Setup(x => x.Timesheets).Returns(mockListTimesheets.Object);

            var controller = new TimesheetsApiController(dbContext.Object);

            var result = await controller.InsertOrUpdateTimesheet(empNumber, endDate, timesheet);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        /* Helper methods and data*/
        List<Timesheet> testTimesheets = new List<Timesheet>()
        {
            new Timesheet {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), StatusName = "Approved", TimesheetRows = new List<TimesheetRow>() { new TimesheetRow { EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02) } } },
            new Timesheet {EmployeeNumber = "1000002", EndDate = new DateTime(2018, 02, 09), StatusName = "Approved"},
            new Timesheet {EmployeeNumber = "1000003", EndDate = new DateTime(2018, 02, 16), StatusName = "Approved"},
            new Timesheet {EmployeeNumber = "1000004", EndDate = new DateTime(2018, 02, 23), StatusName = "Approved"},
            new Timesheet {EmployeeNumber = "1000005", EndDate = new DateTime(2018, 03, 02), StatusName = "Approved"}
        };

        List<Employee> testEmployees = new List<Employee>()
        {
            new Employee {EmployeeNumber = "1000001", FirstName = "Ken", LastName = "McCarthy", Grade = "P1", EmployeeIntials = "KEN", SupervisorNumber = "1000001"},
            new Employee {EmployeeNumber = "1000022", FirstName = "Ron", LastName = "Swanson", Grade = "P2", EmployeeIntials = "RON", SupervisorNumber = "1000002"},
            new Employee {EmployeeNumber = "1000023", FirstName = "Walter", LastName = "White", Grade = "P2", EmployeeIntials = "WW", SupervisorNumber = "1000003"},
            new Employee {EmployeeNumber = "1000024", FirstName = "Jesse", LastName = "Pinkman", Grade = "P2", EmployeeIntials = "JP", SupervisorNumber = "1000004"},
            new Employee {EmployeeNumber = "1000025", FirstName = "Gus", LastName = "Fring", Grade = "P2", EmployeeIntials = "GF", SupervisorNumber = "1000005"}
        };

        List<TimesheetRow> testTimesheetrows = new List<TimesheetRow>()
        {
            new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW090", WorkPackageNumber = "BA7",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 2,Wednesday = 3,Thursday = 0,Friday = 1,},
            new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW8985", WorkPackageNumber = "A125",Saturday = 0,Sunday = 0,Monday = 3,Tuesday = 3,Wednesday = 0,Thursday = 2,Friday = 1,},
            new TimesheetRow {EmployeeNumber = "1000001", EndDate = new DateTime(2018, 02, 02), ProjectNumber = "SW999", WorkPackageNumber = "AA",Saturday = 0,Sunday = 0,Monday = 0,Tuesday = 1,Wednesday = 3,Thursday = 2,Friday = 2,}
        };

        Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new()
        {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

            return dbSetMock;
        }
    }
}
