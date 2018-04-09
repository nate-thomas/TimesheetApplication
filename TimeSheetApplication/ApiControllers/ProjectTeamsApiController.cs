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

        [HttpGet("{pmNumebr}", Name = "GetByEmployeeNumber")]
        public async Task<IActionResult> GetByEmployeeNumber([FromRoute] pmNumebr)
        {
            

            return new ObjectResult(empToReturn);
        }


    }
}