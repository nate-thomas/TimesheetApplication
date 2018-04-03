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
using TimeSheetApplication.Interfaces;
using TimeSheetApplication.Models.TimeSheetSystem;

namespace TimeSheetApplication.ApiControllers
{
    [Produces("application/json")]
    [Route("api/TimesheetRows")]
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    public class TimesheetRowsApiController : Controller
    {
        private readonly IDbContext _context;

        public TimesheetRowsApiController(IDbContext context)
        {
            _context = context;
        }

        // GET: api/TimesheetRows/1234567/2018-02-28
        [HttpGet("{employeeNumber}/{endDate}")]
        public IEnumerable<TimesheetRow> GetTimesheetRowsByEmployeeAndDate([FromRoute] string employeeNumber,
                                                                            [FromRoute] DateTime endDate)
        {
            return _context.TimesheetRows.Where(r => r.EmployeeNumber == employeeNumber && r.EndDate == endDate).ToList();
        }

        // POST: api/TimesheetRows/1234567/2018-02-28
        [HttpPost("{employeeNumber}/{endDate}")]
        public async Task<IActionResult> PostTimesheetRows([FromRoute] string employeeNumber,
                                                           [FromRoute] DateTime endDate,
                                                           [FromBody] List<TimesheetRow> timesheetRows)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (TimesheetRow row in timesheetRows)
            {
                if (row.EmployeeNumber != employeeNumber || row.EndDate != endDate)
                {
                    return BadRequest("Employee number or endDate is incorrect.");
                }
            }

            Timesheet timesheet = new Timesheet { EmployeeNumber = employeeNumber, EndDate = endDate };
            _context.Timesheets.Add(timesheet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_context.Timesheets.Any(e => e.EmployeeNumber == employeeNumber
                                                && e.EndDate == endDate))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                throw;
            }

            _context.TimesheetRows.AddRange(timesheetRows);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                foreach (TimesheetRow row in timesheetRows)
                {
                    if (TimesheetRowsExists(row.EmployeeNumber, row.EndDate, row.ProjectNumber, row.WorkPackageNumber))
                    {
                        return new StatusCodeResult(StatusCodes.Status409Conflict);
                    }
                }
                throw;
            }

            return NoContent();
        }

        // PUT: api/TimesheetRows/1234567/2018-02-28
        [HttpPut("{employeeNumber}/{endDate}")]
        public async Task<IActionResult> UpdateTimesheetRows([FromRoute] string employeeNumber,
                                                             [FromRoute] DateTime endDate,
                                                             [FromBody] List<TimesheetRow> timesheetRows)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (TimesheetRow row in timesheetRows)
            {
                if (row.EmployeeNumber != employeeNumber || row.EndDate != endDate)
                {
                    return BadRequest("Employee number or endDate is incorrect.");
                }
            }

            var deleteRows = _context.TimesheetRows.Where(r => r.EmployeeNumber == employeeNumber && r.EndDate == endDate).ToList();
            if (deleteRows != null)
            {
                _context.TimesheetRows.RemoveRange(deleteRows);
            }
            await _context.SaveChangesAsync();
            _context.TimesheetRows.AddRange(timesheetRows);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/TimesheetRows/1234567/2018-02-28
        [HttpDelete("{employeeNumber}/{endDate}")]
        public async Task<IActionResult> DeleteTimesheetRows([FromRoute] string employeeNumber,
                                                             [FromRoute] DateTime endDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timesheet = _context.Timesheets.Where(t => t.EmployeeNumber == employeeNumber && t.EndDate == endDate).FirstOrDefault();
            var timesheetRows = _context.TimesheetRows.Where(r => r.EmployeeNumber == employeeNumber && r.EndDate == endDate).ToList();
            
            if (timesheet == null || timesheetRows == null)
            {
                return NotFound();
            }

            foreach (TimesheetRow row in timesheetRows)
            {
                _context.TimesheetRows.Remove(row);
            }
            _context.Timesheets.Remove(timesheet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TimesheetRowsExists(string employeeNumber, DateTime endDate, string projectNumber, string workPackageNumber)
        {
            return _context.TimesheetRows.Any(e => e.EmployeeNumber == employeeNumber
                                                && e.EndDate == endDate
                                                && e.ProjectNumber == projectNumber
                                                && e.WorkPackageNumber == workPackageNumber);
        }
    }
}