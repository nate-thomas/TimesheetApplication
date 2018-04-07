using Moq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeSheetApplication.Interfaces;
using TimeSheetApplication.Models.TimeSheetSystem;
using Xunit;
using System.Linq;
using TimeSheetApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using TimeSheetApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using TimeSheetApplication.ViewModels;

namespace XUnitTestProject1
{
    public class EmployeesApiControllerTests
    {
        [Fact]
        public void GetListOfAllEmployeesTest()
        {
            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testEmployees);
            dbContext.Setup(c => c.Employees).Returns(mockList.Object);

            var controller = new EmployeesApiController(dbContext.Object, null, null);
            var resultList = controller.GetAll().ToList();

            Assert.Equal(5, resultList.Count);
        }

        [Theory]
        [InlineData(1000021)]
        [InlineData(1000023)]
        [InlineData(1000025)]
        public void GetEmployeeWithExistingEmployeeNumberTest(long empNumber)
        {
            var user = new ApplicationUser();
            IList<string> roles = new List<string>() { "role1" };

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.FindByNameAsync(empNumber.ToString())).Returns(Task.FromResult(user));
            userManager.Setup(y => y.GetRolesAsync(user)).Returns(Task.FromResult(roles));
            var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);
            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testEmployees);
            dbContext.Setup(c => c.Employees).Returns(mockList.Object);

            var controller = new EmployeesApiController(dbContext.Object, userManager.Object, roleManager.Object);
            var result = controller.GetByEmployeeNumber(empNumber);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result.Result);
        }

        [Fact]
        public void EmployeeNotFoundTest()
        {
            long empNumber = 1000026;
            var user = new ApplicationUser();
            IList<string> roles = null;

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.FindByNameAsync(empNumber.ToString())).Returns(Task.FromResult(user));
            userManager.Setup(y => y.GetRolesAsync(user)).Returns(Task.FromResult(roles));
            var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);
            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testEmployees);
            dbContext.Setup(c => c.Employees).Returns(mockList.Object);

            var controller = new EmployeesApiController(dbContext.Object, userManager.Object, roleManager.Object);
            var result = controller.GetByEmployeeNumber(empNumber);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        //[Fact(Skip ="exception")]
        //public void CreateEmployeeTest()
        //{
        //    Employee emp = testEmployees[0];
        //    var dbContext = new Mock<IDbContext>();
        //    //var mockList = MockDbSet(testEmployees);
        //    //dbContext.Setup(c => c.Employees).Returns(mockList.Object);

        //    var controller = new EmployeesApiController(dbContext.Object);
        //    //Problem: NullReferenceException when the call to CreateEmployee
        //    //Possible Solution: in dbContext.Setup, make the method calls inside CreateEmployee be successful
        //    var result = controller.CreateEmployee(emp);

        //    Assert.NotNull(result);
        //    Assert.IsType<CreatedAtRouteResult>(result);
        //}

        [Fact]
        public void CreateEmployee_WhenModelStateIsInvalid()
        {
            EmployeeViewModel emp = new EmployeeViewModel();
            var controller = new EmployeesApiController(null, null, null);
            //Add error to ModelState error count
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.CreateEmployee(emp);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void CreateEmployee_WhenPasswordsDoNotMatch()
        {
            string expected = "Passwords don't match";
            EmployeeViewModel emp = new EmployeeViewModel { Password = "", ConfirmPassword = "x" };
            var controller = new EmployeesApiController(null, null, null);

            var result = controller.CreateEmployee(emp);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        /* Helper methods and sample data */
        List<Employee> testEmployees = new List<Employee>()
        {
            new Employee {EmployeeNumber = "1000021", FirstName = "Ken", LastName = "McCarthy", Grade = "P1", EmployeeIntials = "KEN", SupervisorNumber = "1000001"},
            new Employee {EmployeeNumber = "1000022", FirstName = "Ron", LastName = "Swanson", Grade = "P2", EmployeeIntials = "RON", SupervisorNumber = "1000001"},
            new Employee {EmployeeNumber = "1000023", FirstName = "Walter", LastName = "White", Grade = "P2", EmployeeIntials = "WW", SupervisorNumber = "1000001"},
            new Employee {EmployeeNumber = "1000024", FirstName = "Jesse", LastName = "Pinkman", Grade = "P2", EmployeeIntials = "JP", SupervisorNumber = "1000001"},
            new Employee {EmployeeNumber = "1000025", FirstName = "Gus", LastName = "Fring", Grade = "P2", EmployeeIntials = "GF", SupervisorNumber = "1000001"}
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
