using Moq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeSheetApplication.Interfaces;
using TimeSheetApplication.Models.TimeSheetSystem;
using Xunit;
using System.Linq;
using TimeSheetApplication.Controllers;

namespace XUnitTestProject1
{
    public class UnitTests
    {
        //[Fact]
        //public void PassTest()
        //{
        //    int val1 = 3;
        //    int val2 = 55;

        //    Assert.Equal(48, Add(val1, val2));
        //}

        //int Add(int num1, int num2)
        //{
        //    return num1 + num2;
        //}

        [Fact]
        public void GetListOfAllEmployees()
        {
            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testEmployees);
            dbContext.Setup(c => c.Employees).Returns(mockList.Object);

            var controller = new EmployeesApiController(dbContext.Object);
            var resultList = controller.GetAll().ToList();

            Assert.Equal(5, resultList.Count);
        }

        List<Employee> testEmployees = new List<Employee>()
        {
            new Employee {EmployeeNumber = "1000021", FirstName = "Ken", LastName = "McCarthy", Grade = "P1", EmployeeIntials = "KEN"},
            new Employee {EmployeeNumber = "1000022", FirstName = "Ron", LastName = "Swanson", Grade = "P2", EmployeeIntials = "RON"},
            new Employee {EmployeeNumber = "1000023", FirstName = "Walter", LastName = "White", Grade = "P2", EmployeeIntials = "WW"},
            new Employee {EmployeeNumber = "1000024", FirstName = "Jesse", LastName = "Pinkman", Grade = "P2", EmployeeIntials = "JP"},
            new Employee {EmployeeNumber = "1000025", FirstName = "Gus", LastName = "Fring", Grade = "P2", EmployeeIntials = "GF"}
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
