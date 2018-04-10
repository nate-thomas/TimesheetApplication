using Microsoft.AspNetCore.Identity;
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
using TimeSheetApplication.Models;
using TimeSheetApplication.Models.TimeSheetSystem;
using Xunit;

namespace XUnitTestProject
{
    public class UsersInRolesApiControllerTests
    {
        [Fact]
        public void GetEmployeesInRole_WhenModelStateIsInvalid()
        {
            string rolename = "role";
            var controller = new UsersInRolesApiController(null, null, null);
            controller.ModelState.AddModelError("key", "message");

            var result = controller.GetEmployeesInRole(rolename);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void GetEmployeesInRole_WhenEmployeesNotFound()
        {
            string rolename = "role";
            IList<ApplicationUser> employees = new List<ApplicationUser>();
            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testEmployees);
            dbContext.Setup(c => c.Employees).Returns(mockList.Object);

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.GetUsersInRoleAsync(rolename)).Returns(Task.FromResult(employees));

            var controller = new UsersInRolesApiController(dbContext.Object, userManager.Object, null);

            var result = controller.GetEmployeesInRole(rolename);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetEmployeesInRole_Successful()
        {
            string rolename = "role";
            IList<ApplicationUser> employees = new List<ApplicationUser>() { new ApplicationUser() { EmployeeNumber = "1000021" }, new ApplicationUser() { EmployeeNumber = "1000022" } };
            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testEmployees);
            dbContext.Setup(c => c.Employees).Returns(mockList.Object);

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.GetUsersInRoleAsync(rolename)).Returns(Task.FromResult(employees));

            var controller = new UsersInRolesApiController(dbContext.Object, userManager.Object, null);

            var result = controller.GetEmployeesInRole(rolename);

            Assert.IsType<OkObjectResult>(result.Result);
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
