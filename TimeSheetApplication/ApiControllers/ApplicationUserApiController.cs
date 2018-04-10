using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimeSheetApplication.Data;
using TimeSheetApplication.Interfaces;
using TimeSheetApplication.Models;
using TimeSheetApplication.ViewModels;

namespace TimeSheetApplication.ApiControllers
{


    [Produces("application/json")]
    [Route("api/ApplicationUser")]
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    public class ApplicationUserApiController : Controller
    {
        private readonly IDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserApiController(IDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        [HttpPut("{employeeNumber}")]
        public async Task<IActionResult> PostPassword([FromRoute] string employeeNumber,
                                                      [FromBody] ApplicationUserViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!item.Password.Equals(item.ConfirmPassword))
            {
                return BadRequest("Passwords don't match");
            }

            var user = await _userManager.FindByNameAsync(employeeNumber);

            if (user != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, item.Password);
                await _userManager.UpdateAsync(user);
            } else
            {
                return NotFound();
            }
            return Ok("Password Updated");
        }
    }
}