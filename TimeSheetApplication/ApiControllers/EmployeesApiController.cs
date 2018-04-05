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

        public EmployeesApiController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        [HttpGet]
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        [HttpGet("{empNumber}", Name = "GetByEmployeeNumber")]
        public IActionResult GetByEmployeeNumber(long empNumber)
        {
            string empNumberStr = empNumber.ToString();

            var item = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, empNumberStr));
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
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

            if (await _userManager.FindByNameAsync(item.EmployeeNumber) == null)
            {

                Employee newEmployee = new Employee
                {
                    EmployeeNumber = item.EmployeeNumber,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Grade = item.Grade,
                    EmployeeIntials = item.EmployeeIntials
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
                    await _userManager.AddToRoleAsync(user, item.Role);
                }

                return CreatedAtRoute("GetByEmployeeNumber", new { empNumber = item.EmployeeNumber }, item);
            }

            return BadRequest("Employee already exists");
        }

        [HttpPut("{empNumber}")]
        public IActionResult Update(long empNumber, [FromBody] Employee item)
        {
            string empNumberStr = empNumber.ToString();
            if (!ModelState.IsValid || !String.Equals(item.EmployeeNumber, empNumberStr))
            {
                return BadRequest(ModelState);
            }

            var employee = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, empNumberStr));
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
            if (item.LaborGrade != null)
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

        [HttpDelete("{empNumber}")]
        public IActionResult Delete(long empNumber)
        {
            string empNumberStr = empNumber.ToString();

            var employee = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, empNumberStr));
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}