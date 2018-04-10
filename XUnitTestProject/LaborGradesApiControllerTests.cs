using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TimeSheetApplication.ApiControllers;
using TimeSheetApplication.Interfaces;
using TimeSheetApplication.Models.TimeSheetSystem;
using Xunit;

namespace XUnitTestProject
{
    public class LaborGradesApiControllerTests
    {
        [Fact]
        public void GetAllLaborGrades()
        {
            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testGrades);
            dbContext.Setup(c => c.LaborGrades).Returns(mockList.Object);

            var controller = new LaborGradesController(dbContext.Object);
            var resultList = controller.GetLaborGrades().ToList();

            Assert.Equal(5, resultList.Count);
        }

        /* Helper methods and sample data */
        List<LaborGrade> testGrades = new List<LaborGrade>()
        {
            new LaborGrade {Grade = "P1", PayAmount = 10},
            new LaborGrade {Grade = "P2", PayAmount = 20},
            new LaborGrade {Grade = "P3", PayAmount = 30},
            new LaborGrade {Grade = "P4", PayAmount = 40},
            new LaborGrade {Grade = "P5", PayAmount = 50}
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
