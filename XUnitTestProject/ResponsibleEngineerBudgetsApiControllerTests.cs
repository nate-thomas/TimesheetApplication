using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ResponsibleEngineerBudgetsApiControllerTests
    {
        [Fact]
        public void GetResponsibleEngineerBudgetsByDateRange_WhenModelStateIsInvalid()
        {
            DateTime fromEndDate = new DateTime();
            DateTime toEndDate = new DateTime();
            var controller = new ResponsibleEngineerBudgetsApiController(null);
            controller.ModelState.AddModelError("key", "message");

            var result = controller.GetResponsibleEngineerBudgetsByDateRange(null, null, fromEndDate, toEndDate);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void GetResponsibleEngineerBudgetsByDateRange_WhenFromEndDateIsNotFriday()
        {
            DateTime fromEndDate = new DateTime(2018, 03, 29);
            DateTime toEndDate = new DateTime(2018, 04, 06);
            string expected = "GetResponsibleEngineerBudgetsByDateRange: end date(s) is not a Friday";
            var controller = new ResponsibleEngineerBudgetsApiController(null);

            var result = controller.GetResponsibleEngineerBudgetsByDateRange(null, null, fromEndDate, toEndDate);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact]
        public void GetResponsibleEngineerBudgetsByDateRange_WhenToEndDateIsNotFriday()
        {
            DateTime fromEndDate = new DateTime(2018, 03, 30);
            DateTime toEndDate = new DateTime(2018, 04, 05);
            string expected = "GetResponsibleEngineerBudgetsByDateRange: end date(s) is not a Friday";
            var controller = new ResponsibleEngineerBudgetsApiController(null);

            var result = controller.GetResponsibleEngineerBudgetsByDateRange(null, null, fromEndDate, toEndDate);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact(Skip = "Issue mocking ToListAsync")]
        public void GetResponsibleEngineerBudgetsByDateRange_Successful()
        {
            DateTime fromEndDate = new DateTime(2018, 03, 30);
            DateTime toEndDate = new DateTime(2018, 04, 06);
            string projectNumber = "WebPrj128";
            string workPackageNumber = "A2";

            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testResponsibleEngineerBudgets);
            dbContext.Setup(c => c.ResponsibleEngineerBudgets).Returns(mockList.Object);

            var controller = new ResponsibleEngineerBudgetsApiController(dbContext.Object);

            var result = controller.GetResponsibleEngineerBudgetsByDateRange(projectNumber, workPackageNumber, fromEndDate, toEndDate);
        }

        [Fact]
        public void InsertOrUpdateResponsibleEngineerBudget_WhenModelStateIsInvalid()
        {
            DateTime fromEndDate = new DateTime();
            var controller = new ResponsibleEngineerBudgetsApiController(null);
            controller.ModelState.AddModelError("key", "message");

            var result = controller.InsertOrUpdateResponsibleEngineerBudget(null, null, fromEndDate, null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void InsertOrUpdateResponsibleEngineerBudget_WhenEndDateIsNotFriday()
        {
            DateTime endDate = new DateTime(2018, 03, 29);
            string expected = "InsertOrUpdateResponsibleEngineerBudget: end date is not a Friday";
            var controller = new ResponsibleEngineerBudgetsApiController(null);

            var result = controller.InsertOrUpdateResponsibleEngineerBudget(null, null, endDate, null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact]
        public void InsertOrUpdateResponsibleEngineerBudget_WhenProjectNumberDoesNotMatch()
        {
            string projectNumber = "abc123";
            string workPackage = "XXX";
            DateTime endDate = new DateTime(2018, 03, 30);
            ResponsibleEngineerBudget reb = new ResponsibleEngineerBudget() { ProjectNumber = "ABC123" };
            string expected = "InsertOrUpdateResponsibleEngineerBudget: ResponsibleEngineerBudget - inconsistent project number and/or work package number and/or end date";

            var controller = new ResponsibleEngineerBudgetsApiController(null);

            var result = controller.InsertOrUpdateResponsibleEngineerBudget(projectNumber, workPackage, endDate, reb);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact]
        public void InsertOrUpdateResponsibleEngineerBudget_WhenWorkPackageDoesNotMatch()
        {
            string projectNumber = "abc123";
            string workPackage = "XXX";
            DateTime endDate = new DateTime(2018, 03, 30);
            ResponsibleEngineerBudget reb = new ResponsibleEngineerBudget() { ProjectNumber = "abc123", WorkPackageNumber = "xxx" };
            string expected = "InsertOrUpdateResponsibleEngineerBudget: ResponsibleEngineerBudget - inconsistent project number and/or work package number and/or end date";

            var controller = new ResponsibleEngineerBudgetsApiController(null);

            var result = controller.InsertOrUpdateResponsibleEngineerBudget(projectNumber, workPackage, endDate, reb);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact]
        public void InsertOrUpdateResponsibleEngineerBudget_WhenEndDateDoesNotMatch()
        {
            string projectNumber = "abc123";
            string workPackage = "XXX";
            DateTime endDate = new DateTime(2018, 03, 30);
            ResponsibleEngineerBudget reb = new ResponsibleEngineerBudget() { ProjectNumber = "abc123", WorkPackageNumber = "XXX", EndDate = new DateTime(2018, 03, 29) };
            string expected = "InsertOrUpdateResponsibleEngineerBudget: ResponsibleEngineerBudget - inconsistent project number and/or work package number and/or end date";

            var controller = new ResponsibleEngineerBudgetsApiController(null);

            var result = controller.InsertOrUpdateResponsibleEngineerBudget(projectNumber, workPackage, endDate, reb);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        /* Helper methods and sample data */
        List<ResponsibleEngineerBudget> testResponsibleEngineerBudgets = new List<ResponsibleEngineerBudget>()
        {
            new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "A2", EndDate = new DateTime(2018, 03, 30) },
            new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "A4", EndDate = new DateTime(2018, 03, 30)},
            new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "AA", EndDate = new DateTime(2018, 03, 30) },
            new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "A11", EndDate = new DateTime(2018, 03, 30)},
            new ResponsibleEngineerBudget {ProjectNumber = "WebPrj128", WorkPackageNumber = "A13", EndDate = new DateTime(2018, 03, 30)}
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
