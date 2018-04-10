//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using AspNet.Security.OAuth.Validation;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using TimeSheetApplication.Data;
using TimeSheetApplication.Models;
//using TimeSheetApplication.Models.TimeSheetSystem;
using TimeSheetApplication.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Data;
using TimeSheetApplication.Models.TimeSheetSystem;

namespace TimeSheetApplication.ApiControllers
{
    [Produces("application/json")]
    [Route("api/ProjectTeams")]
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    public class ProjectTeamsApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProjectTeamsApiController (ApplicationDbContext context,
                                          UserManager<ApplicationUser> userManager,
                                          RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IEnumerable<ProjectTeam> GetAllProjTeams()
        {
            return _context.ProjectTeams.ToList();
        }
        [HttpGet("{projNum}")]
        public async Task<IActionResult> GetEmployeeByProjectNumber([FromRoute] string projNum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<EmployeeViewModel> employeesList = new List<EmployeeViewModel>();

            var projectTeams = _context.ProjectTeams.ToArray();
            foreach (ProjectTeam pt in projectTeams)
            {
                if(pt.ProjectNumber.Equals(projNum))
                {
                    var employee = _context.Employees.FirstOrDefault(emp => String.Equals(emp.EmployeeNumber, pt.EmployeeNumber));
                    var appUser = await _userManager.FindByNameAsync(pt.EmployeeNumber);
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

            if(employeesList.Count == 0)
            {
                return BadRequest("Project not found");
            }
            return new ObjectResult(employeesList);
        }


        [HttpGet("proj/{empNum}")]
        public async Task<IActionResult> GetProjectByEmployeeNumber([FromRoute] string empNum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<string> pojectsList = new List<string>();

            var projectTeams = _context.ProjectTeams.ToArray();
            foreach (ProjectTeam pt in projectTeams)
            {
                if (pt.EmployeeNumber.Equals(empNum))
                {
                    pojectsList.Add(pt.ProjectNumber);
                }
            }

            if (pojectsList.Count == 0)
            {
                return BadRequest("Employee not found");
            }
            return new ObjectResult(pojectsList);
        }

        /* WORK IN PROGRESS -> Not Functional... YET =] */
        /* Work in this method Raymond, feel free to change everything if you think it's better */
        //I just used a delete method. I like destruction -- Raymond
        [HttpPut("{projNum}")]
        public async Task<IActionResult> UpdateEmployeesOfProject([FromRoute] string projNum,
                                                                  [FromBody] List<string> list)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectTeams = _context.ProjectTeams.ToArray();
            /* I think this approach will not work because of FKs */
            /* Remove all Users from the ProjectTeam */
            foreach (ProjectTeam pt in projectTeams)
            {
                if(pt.ProjectNumber != null && pt.ProjectNumber.Equals(projNum))
                {
                    _context.ProjectTeams.Remove(pt);
                    _context.SaveChanges();
                }
            }
            /* Add list of Users to the ProjectTeam */
            foreach (string str in list)
            {
                ProjectTeam newpt = new ProjectTeam
                {
                    ProjectNumber = "projNum",
                    EmployeeNumber = str
                };
                await _context.ProjectTeams.AddAsync(newpt);
                _context.SaveChanges();
            }

            return new NoContentResult();
        }

        // DELETE: api/WPassignmentsAPI/5
        [HttpDelete("{empNo}/{projNo}")]
        public async Task<IActionResult> DeleteProjTeam([FromRoute] string empNo,
            string projNo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<WPassignment> conflictingWorkPackageAssignments = await _context.passignments.Where(w => w.EmployeeNumber == empNo
            && w.ProjectNumber == projNo).ToListAsync();

            foreach(WPassignment item in conflictingWorkPackageAssignments)
            {
                _context.passignments.Remove(item);

            }

            var projectTeam = await _context.ProjectTeams.SingleOrDefaultAsync(m => m.ProjectNumber == projNo
            && m.EmployeeNumber == empNo);
            if (projectTeam == null)
            {
                return NotFound();
            }

            _context.ProjectTeams.Remove(projectTeam);
            await _context.SaveChangesAsync();

            return Ok(projectTeam);
        }
        [HttpPost]
        public async Task<IActionResult> PostProjTeam([FromBody] ProjectTeam projectTeam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProjectTeams.Add(projectTeam);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjectTeamExists(projectTeam.ProjectNumber, projectTeam.EmployeeNumber))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProjectTeam", new { id = projectTeam.ProjectNumber }, projectTeam);
        }
        private bool ProjectTeamExists(string projno, string empno)
        {
            return _context.passignments.Any(e => e.ProjectNumber == projno && e.EmployeeNumber == empno);
        }

    }
}