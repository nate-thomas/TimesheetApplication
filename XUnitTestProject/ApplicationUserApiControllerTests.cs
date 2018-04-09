using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeSheetApplication.ApiControllers;
using TimeSheetApplication.Models;
using TimeSheetApplication.ViewModels;
using Xunit;

namespace XUnitTestProject
{
    public class ApplicationUserApiControllerTests
    {
        [Fact]
        public void PostPassword_WhenModelStateIsInvalid()
        {
            var controller = new ApplicationUserApiController(null, null);
            controller.ModelState.AddModelError("key", "message");

            var result = controller.PostPassword(null, null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void PostPassword_WhenPasswordsDoNotMatch()
        {
            string expected = "Passwords don't match";
            var user = new ApplicationUserViewModel() { Password = "x", ConfirmPassword = "y" };
            var controller = new ApplicationUserApiController(null, null);

            var result = controller.PostPassword(null, user);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequestResult.Value);
        }

        [Fact]
        public void PostPassword_WhenUserNotFound()
        {
            var userModel = new ApplicationUserViewModel() { Password = "x", ConfirmPassword = "x" };
            var empNumberStr = "1000001";
            ApplicationUser user = null;
            
            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.FindByNameAsync(empNumberStr)).Returns(Task.FromResult(user));

            var controller = new ApplicationUserApiController(null, userManager.Object);

            var result = controller.PostPassword(empNumberStr, userModel);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact(Skip = "Cannot mock non-virtual member PasswordHasher")]
        public void PostPassword_Successful()
        {
            var userModel = new ApplicationUserViewModel() { Password = "x", ConfirmPassword = "x" };
            var empNumberStr = "1000001";
            ApplicationUser user = new ApplicationUser() { PasswordHash = "hash" };
            string expected = "Password Updated";

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.FindByNameAsync(empNumberStr)).Returns(Task.FromResult(user));
            userManager.Setup(y => y.PasswordHasher.HashPassword(user, userModel.Password)).Returns(user.PasswordHash);

            var controller = new ApplicationUserApiController(null, userManager.Object);

            var result = controller.PostPassword(empNumberStr, userModel);

            var OkObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expected, OkObjectResult.Value);
        }
    }
}
