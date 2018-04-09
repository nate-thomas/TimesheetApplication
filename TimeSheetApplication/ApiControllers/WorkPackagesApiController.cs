using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Data;
using TimeSheetApplication.Models.TimeSheetSystem;

namespace TimeSheetApplication.ApiControllers
{
    [Produces("application/json")]
    [Route("api/WorkPackages")]
   // [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    public class WorkPackagesApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkPackagesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkPackages
        [HttpGet]
        public IEnumerable<WorkPackage> GetWorkPackages()
        {
            return _context.WorkPackages.ToList();
        }
        // GET: api/WorkPackages/09876
        [HttpGet("{projectNumber}")]
        public IEnumerable<WorkPackage> GetWorkPackageByProjectNumber([FromRoute] string projectNumber)
        {

            return _context.WorkPackages.Where(r => r.ProjectNumber == projectNumber).ToList() as IEnumerable<WorkPackage>;
        }

        // GET: api/WorkPackages/09876%2fA0000
        [HttpGet("{projectNumber}/{workPackageNumber}")]
        public IEnumerable<WorkPackage> GetWorkPackage([FromRoute] string projectNumber, [FromRoute] string workPackageNumber)
        {
            return _context.WorkPackages.Where(r => r.WorkPackageNumber == workPackageNumber && r.ProjectNumber == projectNumber).ToList() as IEnumerable<WorkPackage>;
        }

        // PUT: api/WorkPackages/09876%2fA0000
        [HttpPut("{projectNumber}/{workPackageNumber}")]
        public async Task<IActionResult> PutWorkPackages([FromRoute] string projectNumber, [FromRoute] string workPackageNumber , [FromBody] WorkPackage workPackages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (projectNumber != workPackages.ProjectNumber || workPackageNumber != workPackages.WorkPackageNumber)
            {
                return BadRequest();
            }

            _context.Entry(workPackages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkPackagesExists(projectNumber, workPackageNumber))
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

        // POST: api/WorkPackages
        [HttpPost]
        public async Task<IActionResult> PostWorkPackages([FromBody] WorkPackage workPackages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WorkPackages.Add(workPackages);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WorkPackagesExists(workPackages.ProjectNumber, workPackages.WorkPackageNumber ))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWorkPackages", new { id = workPackages.ProjectNumber }, workPackages);
        }

        // DELETE: api/WorkPackages/09876%2fA0000
        [HttpDelete("{projectNumber}/{workPackageNumber}")]
        public async Task<IActionResult> DeleteWorkPackages([FromRoute] string projectNumber, [FromRoute] string workPackageNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workPackages = await _context.WorkPackages.SingleOrDefaultAsync(m => m.ProjectNumber == projectNumber && m.WorkPackageNumber == workPackageNumber );
            if (workPackages == null)
            {
                return NotFound();
            }

            _context.WorkPackages.Remove(workPackages);
            await _context.SaveChangesAsync();

            return Ok(workPackages);
        }

        private bool WorkPackagesExists(string projectNumber, string workPackageNumber)
        {
            return _context.WorkPackages.Any(e => e.ProjectNumber == projectNumber && e.WorkPackageNumber == workPackageNumber);
        }
    }
}