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
    [Route("api/TimesheetRows")]
    public class TimesheetRowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimesheetRowsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/TimesheetRows/
        [HttpGet]
        public IEnumerable<TimesheetRows> GetTimesheetRows()
        {
            return _context.TimesheetRows.ToList();
        }
        // GET: api/TimesheetRows/1234/02-02-2018
        [HttpGet("{employeeNumber}/{endDate}")]
        public IEnumerable<TimesheetRows> GetTimesheetRowsByEmployeeAndDate([FromRoute] string employeeNumber,
                                                                            [FromRoute] DateTime endDate)
        {
            return _context.TimesheetRows.Where(r => r.EmployeeNumber == employeeNumber && r.EndDate == endDate).ToList();
        }

        //// POST: api/TimesheetRows/1234/02-02-2018
        //[HttpPost("{employeeNumber}/{endDate}")]
        //public async Task<IActionResult> PostTimesheetRows([FromRoute] string employeeNumber,
        //                                                  [FromRoute] DateTime endDate,
        //                                                  [FromBody] List<TimesheetRows> timesheetRows)
        //{
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //foreach (TimesheetRows row in timesheetRows)
            //{
            //    _context.TimesheetRows.Add(row);
            //}

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    foreach (TimesheetRows row in timesheetRows)
            //    {
            //        if (TimesheetRowsExists(row.EmployeeNumber, row.EndDate, row.ProjectNumber, row.WorkPackageNumber))
            //        {
            //            return new StatusCodeResult(StatusCodes.Status409Conflict);
            //        }
            //    }
            //    throw;
            //}

        //    return NoContent();
        //}

        //// DELETE: api/TimesheetRows/1234/02-02-2018
        //[HttpDelete("{employeeNumber}/{endDate}")]
        //public async Task<IActionResult> DeleteTimesheetRows([FromRoute] string employeeNumber,
        //                                                  [FromRoute] DateTime endDate)
        //{
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}


            //var timesheetRows = await _context.TimesheetRows.ToListAsync(r => r.employeeNumber == employeeNumber);
            
            //    if (timesheetRows == null)
            //{
            //    return NotFound();
            //}

            //_context.TimesheetRows.Remove(timesheetRows);

            //await _context.SaveChangesAsync();

        //    return Ok(timesheetRows);
        //}

        private bool TimesheetRowsExists(string employeeNumber, DateTime endDate, string projectNumber, string workPackageNumber)
        {
            return _context.TimesheetRows.Any(e => e.EmployeeNumber == employeeNumber
                                                && e.EndDate == endDate
                                                && e.ProjectNumber == projectNumber
                                                && e.WorkPackageNumber == workPackageNumber);
        }
    }
}