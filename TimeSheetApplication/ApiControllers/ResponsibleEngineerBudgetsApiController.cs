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
    [Route("api/ResponsibleEngineerBudgets")]
    public class ResponsibleEngineerBudgetsApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResponsibleEngineerBudgetsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ResponsibleEngineerBudgets
        [HttpGet]
        public IEnumerable<ResponsibleEngineerBudget> GetResponsibleEngineerBudgets()
        {
            return _context.ResponsibleEngineerBudgets;
        }

        // GET: api/ResponsibleEngineerBudgets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetResponsibleEngineerBudget([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var responsibleEngineerBudget = await _context.ResponsibleEngineerBudgets.SingleOrDefaultAsync(m => m.ProjectNumber == id);

            if (responsibleEngineerBudget == null)
            {
                return NotFound();
            }

            return Ok(responsibleEngineerBudget);
        }

        // PUT: api/ResponsibleEngineerBudgets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResponsibleEngineerBudget([FromRoute] string id, [FromBody] ResponsibleEngineerBudget responsibleEngineerBudget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != responsibleEngineerBudget.ProjectNumber)
            {
                return BadRequest();
            }

            _context.Entry(responsibleEngineerBudget).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponsibleEngineerBudgetExists(id))
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

        private bool ResponsibleEngineerBudgetExists(string id)
        {
            return _context.ResponsibleEngineerBudgets.Any(e => e.ProjectNumber == id);
        }
    }
}