using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeSheetApplication.ApiControllers;
using TimeSheetApplication.Interfaces;
using Xunit;

namespace XUnitTestProject
{
    public class RolesApiControllerTests
    {
        [Fact]
        public void GetAllRolesTest()
        {
            var dbContext = new Mock<IDbContext>();
            var mockRoleStore = new Mock<IQueryableRoleStore<IdentityRole>>();
            var roleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);

            var controller = new RolesApiController(dbContext.Object, roleManager.Object);

            var result = controller.GetAllRoles();

            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
