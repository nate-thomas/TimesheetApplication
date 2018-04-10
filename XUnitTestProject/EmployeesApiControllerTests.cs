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
using System;

namespace XUnitTestProject1
{
    public class EmployeesApiControllerTests
    {
        [Fact]
        public void GetListOfAllEmployeesTest()
        {
            IList<string> roles = new List<string>() { "role1" };
            var employees = testEmployees;

            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testEmployees);
            dbContext.Setup(c => c.Employees).Returns(mockList.Object);

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            for (int i = 0; i < employees.Count; i++)
            {
                var user = GetApplicationUser();
                userManager.Setup(x => x.FindByNameAsync(employees[i].EmployeeNumber)).Returns(Task.FromResult(user));
                userManager.Setup(y => y.GetRolesAsync(user)).Returns(Task.FromResult(roles));
            }

            var controller = new EmployeesApiController(dbContext.Object, userManager.Object, null);
            var result = controller.GetAll();

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result.Result);
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

        [Fact(Skip = "Mock issue")]
        public void CreateEmployee_SuccessfulTest()
        {
            EmployeeViewModel emp = new EmployeeViewModel
            {
                EmployeeNumber = "1000026",
                FirstName = "Henrik",
                LastName = "Sedin",
                Grade = "P1",
                EmployeeIntials = "HS",
                supervisorNumber = "1000001",
                Password = "x",
                ConfirmPassword = "x"
            };
            ApplicationUser user2 = null;
            ApplicationUser user = new ApplicationUser() { UserName = emp.EmployeeNumber };
            IdentityRole role = new IdentityRole();
            ApplicationUser supervisor = new ApplicationUser();
            IdentityResult iResult = IdentityResult.Success;

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.FindByNameAsync(emp.EmployeeNumber)).Returns(Task.FromResult(user2));
            userManager.Setup(z => z.FindByNameAsync(emp.supervisorNumber)).Returns(Task.FromResult(supervisor));
            //Line 147 in the controller; can't mock the CreateAsync call with a local parameter
            userManager.Setup(p => p.CreateAsync(user)).Returns(Task.FromResult(iResult));
            userManager.Setup(j => j.AddPasswordAsync(user, emp.Password)).Returns(Task.FromResult(IdentityResult.Success));
            userManager.Setup(n => n.AddToRoleAsync(user, role.Name)).Returns(Task.FromResult(IdentityResult.Success));

            var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);
            roleManager.Setup(y => y.FindByNameAsync(emp.Role)).Returns(Task.FromResult(role));

            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testEmployees);
            dbContext.Setup(c => c.Employees).Returns(mockList.Object);

            var controller = new EmployeesApiController(dbContext.Object, userManager.Object, roleManager.Object);

            var result = controller.CreateEmployee(emp);

            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

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

        [Fact]
        public void CreateEmployee_WhenSupervisorNumberIsInvalid()
        {
            string expected = "Invalid Supervisor Number";
            EmployeeViewModel emp = new EmployeeViewModel { supervisorNumber = "invalid", Password = "x", ConfirmPassword = "x" };
            ApplicationUser user = new ApplicationUser();
            IdentityRole role = new IdentityRole();

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.FindByNameAsync(emp.EmployeeNumber)).Returns(Task.FromResult(user));

            var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);
            roleManager.Setup(y => y.FindByNameAsync(emp.Role)).Returns(Task.FromResult(role));

            var controller = new EmployeesApiController(null, userManager.Object, roleManager.Object);
            var result = controller.CreateEmployee(emp);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact]
        public void CreateEmployee_WhenRoleIsInvalid()
        {
            string expected = "Invalid Role";
            EmployeeViewModel emp = new EmployeeViewModel { Password = "x", ConfirmPassword = "x" };
            ApplicationUser user = null;
            IdentityRole role = null;

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.FindByNameAsync(emp.EmployeeNumber)).Returns(Task.FromResult(user));

            var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);
            roleManager.Setup(y => y.FindByNameAsync(emp.Role)).Returns(Task.FromResult(role));

            var controller = new EmployeesApiController(null, userManager.Object, roleManager.Object);
            var result = controller.CreateEmployee(emp);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact]
        public void CreateEmployee_WhenEmployeeAlreadyExists()
        {
            string expected = "Employee already exists";
            EmployeeViewModel emp = new EmployeeViewModel { Password = "x", ConfirmPassword = "x" };
            ApplicationUser user = new ApplicationUser();
            IdentityRole role = new IdentityRole();

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.FindByNameAsync(emp.EmployeeNumber)).Returns(Task.FromResult(user));

            var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);
            roleManager.Setup(y => y.FindByNameAsync(emp.Role)).Returns(Task.FromResult(role));

            var controller = new EmployeesApiController(null, userManager.Object, roleManager.Object);
            var result = controller.CreateEmployee(emp);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact]
        public void UpdateEmployeeRole_WhenModelStateIsInvalid()
        {
            string empNumber = "1000001";
            var controller = new EmployeesApiController(null, null, null);
            //Add error to ModelState error count
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.UpdateEmployeeRole(empNumber, null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void UpdateEmployeeRole_WhenEmployeeNumbersDoNotMatch()
        {
            string empNumber = "1000001";
            EmployeeViewModel emp = new EmployeeViewModel { EmployeeNumber = "1000002" };
            var controller = new EmployeesApiController(null, null, null);

            var result = controller.UpdateEmployeeRole(empNumber, emp);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void UpdateEmployeeRole_WhenEmployeeNotFound()
        {
            EmployeeViewModel emp = new EmployeeViewModel { EmployeeNumber = "1000001" };
            string empNumber = "1000001";
            ApplicationUser user = new ApplicationUser();

            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testEmployees);
            dbContext.Setup(c => c.Employees).Returns(mockList.Object);

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.FindByNameAsync(emp.EmployeeNumber)).Returns(Task.FromResult(user));

            var controller = new EmployeesApiController(dbContext.Object, userManager.Object, null);

            var result = controller.UpdateEmployeeRole(empNumber, emp);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void UpdateEmployeeRole_Successful()
        {
            EmployeeViewModel emp = new EmployeeViewModel {
                EmployeeNumber = "1000021"
            };
            string empNumber = "1000021";
            ApplicationUser user = new ApplicationUser();

            var dbContext = new Mock<IDbContext>();
            var mockList = MockDbSet(testEmployees);
            dbContext.Setup(c => c.Employees).Returns(mockList.Object);

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.FindByNameAsync(emp.EmployeeNumber)).Returns(Task.FromResult(user));

            var controller = new EmployeesApiController(dbContext.Object, userManager.Object, null);

            var result = controller.UpdateEmployeeRole(empNumber, emp);

            Assert.IsType<NoContentResult>(result.Result);
        }

        //[Fact]
        //public void Update_WhenModelStateIsInvalid()
        //{
        //    long empNumber = 1000001;
        //    string empRole = "role";

        //    var controller = new EmployeesApiController(null, null, null);
        //    controller.ModelState.AddModelError("key", "error message");

        //    var result = controller.Update(empNumber, empRole);

        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        //    Assert.IsType<SerializableError>(badRequestResult.Value);
        //}

        //[Fact]
        //public void Update_WhenEmployeeNotFound()
        //{
        //    long empNumber = 1000001;
        //    string empRole = "role";
        //    ApplicationUser user = new ApplicationUser();
        //    IdentityRole role = new IdentityRole();
        //    string expected = "Employee not found";

        //    var dbContext = new Mock<IDbContext>();
        //    var mockList = MockDbSet(testEmployees);
        //    dbContext.Setup(c => c.Employees).Returns(mockList.Object);

        //    var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        //    var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
        //    userManager.Setup(x => x.FindByNameAsync(empNumber.ToString())).Returns(Task.FromResult(user));

        //    var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
        //    var roleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);
        //    roleManager.Setup(y => y.FindByNameAsync(empRole)).Returns(Task.FromResult(role));

        //    var controller = new EmployeesApiController(dbContext.Object, userManager.Object, roleManager.Object);

        //    var result = controller.Update(empNumber, empRole);

        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        //    Assert.Equal(expected, badRequestResult.Value);
        //}

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

        private ApplicationUser GetApplicationUser()
        {
            return new ApplicationUser();
        }
    }
}
