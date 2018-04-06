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
    [Route("api/Timesheets")]
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    public class TimesheetsApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimesheetsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TimesheetsApi/1000010
        [HttpGet("{employeeNumber}")]
        public async Task<IActionResult> GetTimesheetsByEmployeeNumber([FromRoute] string employeeNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timesheets = await _context.Timesheets.Where(m => m.EmployeeNumber == employeeNumber).ToListAsync();
            
            if (timesheets == null)
            {
                return NotFound();
            }

            return Ok(timesheets);
        }

        // GET: api/TimesheetsApi/1000010/2018-02-09
        [HttpGet("{employeeNumber}/{endDate}")]
        public async Task<IActionResult> GetTimesheetByEmployeeNumberAndEndDate([FromRoute] string employeeNumber, DateTime endDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (endDate.DayOfWeek != DayOfWeek.Friday)
            {
                return BadRequest("InsertOrUpdateTimesheet: end date is not a Friday");
            }

            var timesheet = await _context.Timesheets
                .Include(m => m.TimesheetRows)
                .SingleOrDefaultAsync(m => m.EmployeeNumber == employeeNumber && m.EndDate == endDate);
            
            if (timesheet == null)
            {
                return NotFound();
            }

            return Ok(timesheet);
        }

        // PUT: api/TimesheetsApi/1000010/2018-02-09
        [HttpPut("{employeeNumber}/{endDate}")]
        public async Task<IActionResult> InsertOrUpdateTimesheet([FromRoute] string employeeNumber, DateTime endDate, [FromBody] Timesheet timesheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (endDate.DayOfWeek != DayOfWeek.Friday)
            {
                return BadRequest("InsertOrUpdateTimesheet: end date is not a Friday");
            }

            if (employeeNumber != timesheet.EmployeeNumber || endDate != timesheet.EndDate)
            {
                return BadRequest("InsertOrUpdateTimesheet: inconsistent timesheet employee number and/or end date");
            }

            // TODO: If any timesheet row's employee number and end date is inconsistent, return bad request

            var existingTimesheet = await _context.Timesheets
                .Include(t => t.TimesheetRows)
                .FirstOrDefaultAsync(t => t.EmployeeNumber == employeeNumber && t.EndDate == endDate);

            if (existingTimesheet == null)
            {
                _context.Add(timesheet);
            }
            else
            {
                _context.Entry(existingTimesheet).CurrentValues.SetValues(timesheet);
                foreach (var timesheetRow in timesheet.TimesheetRows)
                {
                    if (employeeNumber != timesheetRow.EmployeeNumber || endDate != timesheetRow.EndDate)
                    {
                        return BadRequest("InsertOrUpdateTimesheet: inconsistent timesheet.timesheetRows - employee number and/or end date");
                    }

                    var existingTimesheetRow = existingTimesheet.TimesheetRows
                        .FirstOrDefault(r => r.EmployeeNumber == timesheetRow.EmployeeNumber
                                          && r.EndDate == timesheetRow.EndDate
                                          && r.ProjectNumber == timesheetRow.ProjectNumber
                                          && r.WorkPackageNumber == timesheetRow.WorkPackageNumber
                        );
                    
                    if (existingTimesheetRow == null)
                    {
                        existingTimesheet.TimesheetRows.Add(timesheetRow);
                    }
                    else
                    {
                        _context.Entry(existingTimesheetRow).CurrentValues.SetValues(timesheetRow);
                    }
                }
            }
            
            try
            {
                await _context.SaveChangesAsync();
            }
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!TimesheetExists(employeeNumber, endDate))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            catch (DbUpdateException)
            {
                if (TimesheetExists(employeeNumber, endDate))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //// DELETE: api/TimesheetsApi/1000010/2018-02-09
        //[HttpDelete("{employeeNumber}/{endDate}")]
        //public async Task<IActionResult> DeleteTimesheet([FromRoute] string employeeNumber, DateTime endDate)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var timesheet = await _context.Timesheets.SingleOrDefaultAsync(m => m.EmployeeNumber == employeeNumber && m.EndDate == endDate);
        //    if (timesheet == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Timesheets.Remove(timesheet);
        //    await _context.SaveChangesAsync();

        //    return Ok(timesheet);
        //}

        private bool TimesheetExists(string employeeNumber, DateTime endDate)
        {
            return _context.Timesheets.Any(e => e.EmployeeNumber == employeeNumber && e.EndDate == endDate);
        }
    }
}