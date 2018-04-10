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
    [Route("api/ResponsibleEngineerBudgets")]
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    public class ResponsibleEngineerBudgetsApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResponsibleEngineerBudgetsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ResponsibleEngineerBudgets/version
        [HttpGet("version")]
        public string GetVersion()
        {
            return "Version 1.0";
        }

        //// GET: api/ResponsibleEngineerBudgets
        //[HttpGet]
        //public IEnumerable<ResponsibleEngineerBudget> GetResponsibleEngineerBudgets()
        //{
        //    return _context.ResponsibleEngineerBudgets;
        //}

        // GET: api/ResponsibleEngineerBudgets/WebPrj128/A2/2018-03-23/2018-03-30
        [HttpGet("{projectNumber}/{workPackageNumber}/{fromEndDate}/{toEndDate}")]
        [Authorize(Roles = "Administrator, Project Manager, Responsible Engineer")]
        public async Task<IActionResult> GetResponsibleEngineerBudgetsByDateRange([FromRoute] string projectNumber, [FromRoute] string workpackageNumber, [FromRoute] DateTime fromEndDate, [FromRoute] DateTime toEndDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (fromEndDate.DayOfWeek != DayOfWeek.Friday || toEndDate.DayOfWeek != DayOfWeek.Friday)
            {
                return BadRequest("GetResponsibleEngineerBudgetsByDateRange: end date(s) is not a Friday");
            }

            var responsibleEngineerBudgets = await _context.ResponsibleEngineerBudgets
                .Include(r => r.REBbyGrade)
                .Select(r => new {
                    r.ProjectNumber,
                    r.WorkPackageNumber,
                    r.EndDate,
                    REBbyGrade = r.REBbyGrade.Select(g => new
                    {
                        g.Grade,
                        g.EstimatedManHours
                    })
                })
                .Where(r => r.ProjectNumber == projectNumber && r.WorkPackageNumber == workpackageNumber && r.EndDate >= fromEndDate && r.EndDate <= toEndDate)
                .ToListAsync();

            if (responsibleEngineerBudgets == null)
            {
                return NotFound();
            }

            return Ok(responsibleEngineerBudgets);
        }

        // PUT: api/ResponsibleEngineerBudgets/WebPrj128/A2/2018-03-23
        [HttpPut("{projectNumber}/{workPackageNumber}/{endDate}")]
        [Authorize(Roles = "Administrator, Project Manager, Responsible Engineer")]
        public async Task<IActionResult> InsertOrUpdateResponsibleEngineerBudget([FromRoute] string projectNumber, [FromRoute] string workPackageNumber, [FromRoute] DateTime endDate, [FromBody] ResponsibleEngineerBudget reb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (endDate.DayOfWeek != DayOfWeek.Friday)
            {
                return BadRequest("InsertOrUpdateResponsibleEngineerBudget: end date is not a Friday");
            }

            if (projectNumber != reb.ProjectNumber || workPackageNumber != reb.WorkPackageNumber || endDate != reb.EndDate)
            {
                return BadRequest("InsertOrUpdateResponsibleEngineerBudget: ResponsibleEngineerBudget - inconsistent project number and/or work package number and/or end date");
            }
            
            var existingREB = await _context.ResponsibleEngineerBudgets
                .Include(r => r.REBbyGrade)
                .FirstOrDefaultAsync(r => r.ProjectNumber == projectNumber && r.WorkPackageNumber == workPackageNumber && r.EndDate == endDate);

            if (existingREB == null)
            {
                _context.Add(reb);
            }
            else
            {
                _context.Entry(existingREB).CurrentValues.SetValues(reb);
                foreach (var rebg in reb.REBbyGrade)
                {
                    if (projectNumber != rebg.ProjectNumber || workPackageNumber != rebg.WorkPackageNumber || endDate != rebg.EndDate)
                    {
                        return BadRequest("InsertOrUpdateResponsibleEngineerBudget: ResponsibleEngineerBudget.REBbyGrade - inconsistent project number and/or work package number and/or end date");
                    }

                    var existingREBbyGrade = existingREB.REBbyGrade
                        .FirstOrDefault(g => g.ProjectNumber == rebg.ProjectNumber
                            && g.WorkPackageNumber == rebg.WorkPackageNumber
                            && g.EndDate == rebg.EndDate
                            && g.Grade == rebg.Grade
                        );

                    if (existingREBbyGrade == null)
                    {
                        existingREB.REBbyGrade.Add(rebg);
                    }
                    else
                    {
                        _context.Entry(existingREBbyGrade).CurrentValues.SetValues(rebg);
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
                if (ResponsibleEngineerBudgetExists(projectNumber, workPackageNumber, endDate))
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

        //// POST: api/ResponsibleEngineerBudgets
        //[HttpPost]
        //public async Task<IActionResult> PostResponsibleEngineerBudget([FromBody] ResponsibleEngineerBudget responsibleEngineerBudget)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.ResponsibleEngineerBudgets.Add(responsibleEngineerBudget);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ResponsibleEngineerBudgetExists(responsibleEngineerBudget.ProjectNumber))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetResponsibleEngineerBudget", new { id = responsibleEngineerBudget.ProjectNumber }, responsibleEngineerBudget);
        //}

        //// DELETE: api/ResponsibleEngineerBudgets/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteResponsibleEngineerBudget([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var responsibleEngineerBudget = await _context.ResponsibleEngineerBudgets.SingleOrDefaultAsync(m => m.ProjectNumber == id);
        //    if (responsibleEngineerBudget == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.ResponsibleEngineerBudgets.Remove(responsibleEngineerBudget);
        //    await _context.SaveChangesAsync();

        //    return Ok(responsibleEngineerBudget);
        //}

        private bool ResponsibleEngineerBudgetExists(string projectNumber, string workPackageNumber, DateTime endDate)
        {
            return _context.ResponsibleEngineerBudgets.Any(e => e.ProjectNumber == projectNumber && e.WorkPackageNumber == workPackageNumber && e.EndDate == endDate);
        }
    }
}