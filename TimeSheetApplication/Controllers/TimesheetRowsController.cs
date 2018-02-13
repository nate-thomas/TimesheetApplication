using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeSheetApplication.Data;
using TimeSheetApplication.Models.TimeSheetSystem;

namespace TimeSheetApplication.Controllers
{
    public class TimesheetRowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimesheetRowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TimesheetRows
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TimesheetRows.Include(t => t.Timesheet).Include(t => t.WorkPackage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TimesheetRows/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheetRows = await _context.TimesheetRows
                .Include(t => t.Timesheet)
                .Include(t => t.WorkPackage)
                .SingleOrDefaultAsync(m => m.EmployeeNumber == id);
            if (timesheetRows == null)
            {
                return NotFound();
            }

            return View(timesheetRows);
        }

        // GET: TimesheetRows/Create
        public IActionResult Create()
        {
            ViewData["EmployeeNumber"] = new SelectList(_context.Timesheets, "EmployeeNumber", "EmployeeNumber");
            ViewData["ProjectNumber"] = new SelectList(_context.WorkPackages, "ProjectNumber", "ProjectNumber");
            return View();
        }

        // POST: TimesheetRows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimesheetRowsId,EmployeeNumber,EndDate,ProjectNumber,WorkPackageNumber,Saturday,Sunday,Monday,Tuesday,Wednesday,Thursday,Friday")] TimesheetRows timesheetRows)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timesheetRows);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeNumber"] = new SelectList(_context.Timesheets, "EmployeeNumber", "EmployeeNumber", timesheetRows.EmployeeNumber);
            ViewData["ProjectNumber"] = new SelectList(_context.WorkPackages, "ProjectNumber", "ProjectNumber", timesheetRows.ProjectNumber);
            return View(timesheetRows);
        }

        // GET: TimesheetRows/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheetRows = await _context.TimesheetRows.SingleOrDefaultAsync(m => m.EmployeeNumber == id);
            if (timesheetRows == null)
            {
                return NotFound();
            }
            ViewData["EmployeeNumber"] = new SelectList(_context.Timesheets, "EmployeeNumber", "EmployeeNumber", timesheetRows.EmployeeNumber);
            ViewData["ProjectNumber"] = new SelectList(_context.WorkPackages, "ProjectNumber", "ProjectNumber", timesheetRows.ProjectNumber);
            return View(timesheetRows);
        }

        // POST: TimesheetRows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TimesheetRowsId,EmployeeNumber,EndDate,ProjectNumber,WorkPackageNumber,Saturday,Sunday,Monday,Tuesday,Wednesday,Thursday,Friday")] TimesheetRows timesheetRows)
        {
            if (id != timesheetRows.EmployeeNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timesheetRows);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimesheetRowsExists(timesheetRows.EmployeeNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeNumber"] = new SelectList(_context.Timesheets, "EmployeeNumber", "EmployeeNumber", timesheetRows.EmployeeNumber);
            ViewData["ProjectNumber"] = new SelectList(_context.WorkPackages, "ProjectNumber", "ProjectNumber", timesheetRows.ProjectNumber);
            return View(timesheetRows);
        }

        // GET: TimesheetRows/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheetRows = await _context.TimesheetRows
                .Include(t => t.Timesheet)
                .Include(t => t.WorkPackage)
                .SingleOrDefaultAsync(m => m.EmployeeNumber == id);
            if (timesheetRows == null)
            {
                return NotFound();
            }

            return View(timesheetRows);
        }

        // POST: TimesheetRows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var timesheetRows = await _context.TimesheetRows.SingleOrDefaultAsync(m => m.EmployeeNumber == id);
            _context.TimesheetRows.Remove(timesheetRows);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimesheetRowsExists(string id)
        {
            return _context.TimesheetRows.Any(e => e.EmployeeNumber == id);
        }
    }
}
