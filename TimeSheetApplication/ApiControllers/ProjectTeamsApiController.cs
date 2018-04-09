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

        [HttpGet("{projNum}")]
        public async Task<IActionResult> GetEmployeeByProjectNumber([FromRoute] string projNum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<string> employeesNumberList = new List<string>();

            var projectTeams = _context.ProjectTeams.ToArray();
            foreach (ProjectTeam pt in projectTeams)
            {
                if(pt.ProjectNumber.Equals(projNum))
                {
                    employeesNumberList.Add(pt.EmployeeNumber);
                }
            }

            if(employeesNumberList.Count == 0)
            {
                return BadRequest("Project not found");
            }
            return new ObjectResult(employeesNumberList);
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

    }
}