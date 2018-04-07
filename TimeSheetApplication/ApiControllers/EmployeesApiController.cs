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
using TimeSheetApplication.Models.TimeSheetSystem;
using TimeSheetApplication.ViewModels;

namespace TimeSheetApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/Employees")]
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    public class EmployeesApiController : Controller
    {
        private readonly IDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeesApiController(IDbContext context,
                                      UserManager<ApplicationUser> userManager,
                                      RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        [HttpGet("{empNumber}", Name = "GetByEmployeeNumber")]
        public async Task<IActionResult> GetByEmployeeNumber(long empNumber)
        {
            string empNumberStr = empNumber.ToString();

            var employee = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, empNumberStr));
            var appUser = await _userManager.FindByNameAsync(empNumberStr);
            var userRole = await _userManager.GetRolesAsync(appUser);

            if (employee == null || appUser == null || userRole == null)
            {
                return NotFound();
            }

            EmployeeViewModel empToReturn = new EmployeeViewModel
            {
                EmployeeNumber = employee.EmployeeNumber,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Grade = employee.Grade,
                EmployeeIntials = employee.EmployeeIntials,
                Password = "",
                ConfirmPassword = "",
                Role = userRole[0],
                supervisorNumber = employee.SupervisorNumber
            };

            return new ObjectResult(empToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!item.Password.Equals(item.ConfirmPassword))
            {
                return BadRequest("Passwords don't match");
            }

            var appUser = await _userManager.FindByNameAsync(item.EmployeeNumber);
            var userRole = await _roleManager.FindByNameAsync(item.Role);
            
            /* Check if supervisor exists */
            if (item.supervisorNumber != null)
            {
                var appUserSupervisor = await _userManager.FindByNameAsync(item.supervisorNumber);
                if (appUserSupervisor == null)
                {
                    return BadRequest("Invalid Supervisor Number");
                }
            }

            if (appUser == null)
            {
                if (userRole == null)
                {
                    return BadRequest("Invalid Role");
                }

                Employee newEmployee = new Employee
                {
                    EmployeeNumber = item.EmployeeNumber,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Grade = item.Grade,
                    EmployeeIntials = item.EmployeeIntials,
                    SupervisorNumber = item.supervisorNumber
                };

                _context.Employees.Add(newEmployee);
                await _context.SaveChangesAsync();

                var user = new ApplicationUser
                {
                    UserName = item.EmployeeNumber,
                    EmployeeNumber = item.EmployeeNumber
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(user, item.Password);
                    await _userManager.AddToRoleAsync(user, userRole.Name);
                }

                return CreatedAtRoute("GetByEmployeeNumber", new { empNumber = item.EmployeeNumber }, item);
            }

            return BadRequest("Employee already exists");
        }

        /* Update Basic Employee Information -Not password or Role- */
        [HttpPut("{empNumber}")]
        public async Task<IActionResult> UpdateEmployeeRole(long empNumber, [FromBody] Employee item)
        {
            string empNumberStr = empNumber.ToString();
            if (!ModelState.IsValid || !String.Equals(item.EmployeeNumber, empNumberStr))
            {
                return BadRequest(ModelState);
            }

            var employee = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, empNumberStr));
            var appUser = await _userManager.FindByNameAsync(item.EmployeeNumber);

            if (employee == null)
            {
                return NotFound();
            }

            //Set First Name if asked to
            if (item.FirstName != null)
            {
                employee.FirstName = item.FirstName;
            }
            //Set Last Name if asked to
            if (item.LastName != null)
            {
                employee.LastName = item.LastName;
            }
            //Set LaborGrade if asked to
            if (item.Grade != null)
            {
                employee.LaborGrade = item.LaborGrade;
            }
            //Set Grade if asked to
            if (item.Grade != null)
            {
                employee.Grade = item.Grade;
            }
            //Set EmployeeIntials if asked to
            if (item.EmployeeIntials != null)
            {
                employee.EmployeeIntials = item.EmployeeIntials;
            }
            //Set Supervisor if asked to
            if (item.Supervisor != null)
            {
                employee.Supervisor = item.Supervisor;
            }
            //Set SupervisorNumber if asked to
            if (item.SupervisorNumber != null)
            {
                employee.SupervisorNumber = item.SupervisorNumber;
            }
            //Set Timesheets if asked to
            if (item.Timesheets != null)
            {
                employee.Timesheets = item.Timesheets;
            }
            //Check if new model doesn't break any FK Constraints
            try
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return new NoContentResult();
            }
            catch (Exception e)
            {
                return BadRequest(ModelState);
            }
        }

        /* Update Employee Role */
        [HttpPut("{empNumber}/{empRole}")]
        public async Task<IActionResult> Update([FromRoute] long empNumber,
                                                [FromRoute] string empRole)
        {
            string empNumberStr = empNumber.ToString();
            string fixRole = empRole.Replace('-',' ');

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, empNumberStr));
            var appUser = await _userManager.FindByNameAsync(empNumberStr);
            var newRole = await _roleManager.FindByNameAsync(fixRole);

            if (employee != null && appUser != null)
            {
                if(newRole == null)
                {
                    return BadRequest("Invalid Role");
                }
                await _userManager.AddToRoleAsync(appUser, fixRole);
                return new NoContentResult();
            }
            return BadRequest("Employee not found");
           
        }

        [HttpDelete("{empNumber}")]
        public async Task<IActionResult> Delete(long empNumber)
        {
            string empNumberStr = empNumber.ToString();

            var employee = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, empNumberStr));
            var appUser = await _userManager.FindByNameAsync(empNumberStr);

            if (employee == null || appUser == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            await _userManager.DeleteAsync(appUser);

            return new NoContentResult();
        }
    }
}