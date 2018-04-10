using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeSheetApplication.ApiControllers;
using TimeSheetApplication.Interfaces;
using TimeSheetApplication.Models.TimeSheetSystem;
using Xunit;

namespace XUnitTestProject
{
    public class WorkPackagesApiControllerTests
    {
        [Fact]
        public void GetAllWorkPackages()
        {
            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testWorkPackages);
            dbContext.Setup(c => c.WorkPackages).Returns(mockList.Object);

            var controller = new WorkPackagesApiController(dbContext.Object);
            var resultList = controller.GetWorkPackages().ToList();

            Assert.Equal(5, resultList.Count);
        }

        /* Helper methods and data */
        List<WorkPackage> testWorkPackages = new List<WorkPackage>()
        {
            new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "A", Description = "Project#:WebPrj128 WorkingPackage#:A", Budget=6000},
            new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "A1", Description = "Project#:WebPrj128 WorkingPackage#:A1", Budget=1500},
            new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "A2", Description = "Project#:WebPrj128 WorkingPackage#:A2", Budget=1500,ResponsibleEngineerNumber = "1000004"},
            new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "A4", Description = "Project#:WebPrj128 WorkingPackage#:A4", Budget=750,ResponsibleEngineerNumber = "1000004"},
            new WorkPackage {ProjectNumber = "WebPrj128", WorkPackageNumber = "AA", Description = "Project#:WebPrj128 WorkingPackage#:AA", Budget=2250,ResponsibleEngineerNumber = "1000004"}
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
