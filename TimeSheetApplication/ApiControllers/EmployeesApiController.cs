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
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeesApiController(ApplicationDbContext context,
                                      UserManager<ApplicationUser> userManager,
                                      RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<EmployeeViewModel> employeesList = new List<EmployeeViewModel>();
            var employee = _context.Employees.ToArray<Employee>();
            foreach (Employee emp in employee)
            {

                var appUser = await _userManager.FindByNameAsync(emp.EmployeeNumber);
                var userRole = await _userManager.GetRolesAsync(appUser);

                EmployeeViewModel temp = new EmployeeViewModel
                {
                    EmployeeNumber = emp.EmployeeNumber,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Grade = emp.Grade,
                    EmployeeIntials = emp.EmployeeIntials,
                    Password = "",
                    ConfirmPassword = "",
                    Role = userRole[0],
                    supervisorNumber = emp.SupervisorNumber
                };
                employeesList.Add(temp);
            }
            return new ObjectResult(employeesList);
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

        [HttpGet("sup/{supNumber}")]
        public async Task<IActionResult> GetByForSupervisor([FromRoute] string supNumber)
        {
            List<EmployeeViewModel> employeesList = new List<EmployeeViewModel>();
            var employee = _context.Employees.ToArray<Employee>();
            foreach (Employee emp in employee)
            {
                if (emp.SupervisorNumber != null && emp.SupervisorNumber.Equals(supNumber))
                {
                    var appUser = await _userManager.FindByNameAsync(emp.EmployeeNumber);
                    var userRole = await _userManager.GetRolesAsync(appUser);

                    EmployeeViewModel temp = new EmployeeViewModel
                    {
                        EmployeeNumber = emp.EmployeeNumber,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        Grade = emp.Grade,
                        EmployeeIntials = emp.EmployeeIntials,
                        Password = "",
                        ConfirmPassword = "",
                        Role = userRole[0],
                        supervisorNumber = emp.SupervisorNumber
                    };
                    employeesList.Add(temp);
                }
            }
            if(employeesList.Count == 0)
            {
                return BadRequest("Supervisor not found");
            }
            return new ObjectResult(employeesList);
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

        /* NEW VERSION FOR UPDATING EMPLOYEE */
        [HttpPut("{empNumber}")]
        public async Task<IActionResult> UpdateEmployeeRole([FromRoute] string empNumber, [FromBody] EmployeeViewModel item)
        {
            if (!ModelState.IsValid || !String.Equals(item.EmployeeNumber, empNumber))
            {
                return BadRequest(ModelState);
            }

            var employee = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, empNumber));
            var appUser = await _userManager.FindByNameAsync(item.EmployeeNumber);

            if (employee == null)
            {
                return NotFound("Employee not Found");
            }

            //Get new Role
            var newRole = await _roleManager.FindByNameAsync(item.Role);

            if (newRole == null)
            {
                return NotFound("Invalid Role");
            }

            //Validation on front-end
            //Update fields
            employee.FirstName = item.FirstName;
            employee.LastName = item.LastName;
            employee.Grade = item.Grade;
            employee.EmployeeIntials = item.EmployeeIntials;
            employee.SupervisorNumber = item.supervisorNumber;

            //update role and remove previous role
            var test = await _userManager.GetRolesAsync(appUser);
            foreach (var r in test)
            {
                await _userManager.RemoveFromRoleAsync(appUser, r);
            }
            
            await _userManager.AddToRoleAsync(appUser, item.Role);

            var test2 = await _userManager.GetRolesAsync(appUser);

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