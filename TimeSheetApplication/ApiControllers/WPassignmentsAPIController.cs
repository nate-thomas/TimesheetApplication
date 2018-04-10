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
    [Route("api/WPassignments")]
    public class WPassignmentsAPIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WPassignmentsAPIController(ApplicationDbContext context)
        {
            _context = context;
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
        public IEnumerable<WPassignment> GetEmployeesFromWPandProjectAssignments([FromRoute] string projectNumber,
            string wpNumber)
        {
            return _context.WPassignments.Where(r => r.WorkPackageNumber == wpNumber && r.ProjectNumber == projectNumber).ToList() as IEnumerable<WPassignment>;
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