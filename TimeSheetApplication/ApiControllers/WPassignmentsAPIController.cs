using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Data;
using TimeSheetApplication.Models;
using TimeSheetApplication.Models.TimeSheetSystem;
using TimeSheetApplication.ViewModels;

namespace TimeSheetApplication.ApiControllers
{
    [Produces("application/json")]
    [Route("api/WPassignments")]
    public class WPassignmentsAPIController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public WPassignmentsAPIController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/WPassignmentsAPI
        [HttpGet]
        public IEnumerable<WPassignment> GetWPassignments()
        {
            return _context.WPassignments;
        }
        //5 - Retrieve list of employees on a WP Assignment receiving a ProjNumber 
        //and a WorkPackageNumber (WorkPackagesApiController)
        //GET: api:WPassignments/Employees/PN/WP
        [HttpGet("Employees/{projectNumber}/{wpNumber}")]
        public async Task<IActionResult> GetEmployeesFromWPandProjectAssignments([FromRoute] string projectNumber,
            string wpNumber)
        {
            List<EmployeeViewModel> employeesList = new List<EmployeeViewModel>();
            var WpAssignments = _context.WPassignments.ToArray();
            foreach (WPassignment wpa in WpAssignments)
            {
                if (wpa.ProjectNumber.Equals(projectNumber) && wpa.WorkPackageNumber.Equals(wpNumber))
                {
                    var employee = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, wpa.EmployeeNumber));
                    var appUser = await _userManager.FindByNameAsync(wpa.EmployeeNumber);
                    var userRole = await _userManager.GetRolesAsync(appUser);
                    EmployeeViewModel temp = new EmployeeViewModel
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
                    employeesList.Add(temp);
                }
            }

            if (employeesList.Count == 0)
            {
                return BadRequest("Project not found");
            }
            return new ObjectResult(employeesList);
        }

        // GET: api/WPassignments/All/EN
        [HttpGet("All/{employeeNumber}")]
        public IEnumerable<WPassignment> GetWPassignmentsforEmployee([FromRoute] string projectNumber,
            string employeeNumber)
        {
            return _context.WPassignments.Where(r => r.EmployeeNumber == employeeNumber).ToList() as IEnumerable<WPassignment>;
        }

        // GET: api/WPassignments/PN/EN
        [HttpGet("{projectNumber}/{employeeNumber}")]
        public IEnumerable <WPassignment> GetWPProjectassignmentforEmployee([FromRoute] string projectNumber,
            string employeeNumber)
        {
            return _context.WPassignments.Where(r => r.EmployeeNumber == employeeNumber && r.ProjectNumber == projectNumber).ToList() as IEnumerable<WPassignment>;
        }

        // PUT: api/WPassignmentsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWPassignment([FromRoute] string id, [FromBody] WPassignment wPassignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wPassignment.ProjectNumber)
            {
                return BadRequest();
            }

            _context.Entry(wPassignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WPassignmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WPassignmentsAPI
        [HttpPost]
        public async Task<IActionResult> PostWPassignment([FromBody] WPassignment wPassignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WPassignments.Add(wPassignment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WPassignmentExists(wPassignment.ProjectNumber))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWPassignment", new { id = wPassignment.ProjectNumber }, wPassignment);
        }

        // DELETE: api/WPassignmentsAPI/5
        [HttpDelete("{empNo}/{projNo}/{wpNo}")]
        public async Task<IActionResult> DeleteWPassignment([FromRoute] string empNo,
            string projNo, string wpNo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var wPassignment = await _context.WPassignments.SingleOrDefaultAsync(m => m.ProjectNumber == projNo
            && m.WorkPackageNumber == wpNo && m.EmployeeNumber == empNo);
            if (wPassignment == null)
            {
                return NotFound();
            }

            _context.WPassignments.Remove(wPassignment);
            await _context.SaveChangesAsync();

            return Ok(wPassignment);
        }

        private bool WPassignmentExists(string id)
        {
            return _context.WPassignments.Any(e => e.ProjectNumber == id);
        }
    }
}