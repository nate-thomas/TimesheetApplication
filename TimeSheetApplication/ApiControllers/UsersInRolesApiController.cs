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

namespace TimeSheetApplication.ApiControllers
{
    [Produces("application/json")]
    [Route("api/UsersInRoles")]
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    public class UsersInRolesApiController : Controller
    {
        private readonly IDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersInRolesApiController(IDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/UsersInRoles/Supervisor
        [HttpGet("{roleName}")]
        public async Task<IActionResult> GetEmployeesInRole([FromRoute] string roleName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _userManager.GetUsersInRoleAsync(roleName);

            var employees = users.Join(_context.Employees, 
                u => u.EmployeeNumber, 
                e => e.EmployeeNumber, 
                (u, e) => new {
                    EmployeeNumber = e.EmployeeNumber,
                    EmployeeName = e.FirstName + ' ' + e.LastName
                })
                .OrderBy(e => e.EmployeeNumber);

            if (!employees.Any())
            {
                return NotFound();
            }

            return Ok(employees);
        }
    }
}