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
    public class TimesheetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimesheetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Timesheets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Timesheets.Include(t => t.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Timesheets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheets = await _context.Timesheets
                .Include(t => t.Employee)
                .SingleOrDefaultAsync(m => m.EmployeeNumber == id);
            if (timesheets == null)
            {
                return NotFound();
            }

            return View(timesheets);
        }

        // GET: Timesheets/Create
        public IActionResult Create()
        {
            ViewData["EmployeeNumber"] = new SelectList(_context.Employees, "EmployeeNumber", "EmployeeNumber");
            return View();
        }

        // POST: Timesheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeNumber,EndDate")] Timesheets timesheets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timesheets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeNumber"] = new SelectList(_context.Employees, "EmployeeNumber", "EmployeeNumber", timesheets.EmployeeNumber);
            return View(timesheets);
        }

        // GET: Timesheets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheets = await _context.Timesheets.SingleOrDefaultAsync(m => m.EmployeeNumber == id);
            if (timesheets == null)
            {
                return NotFound();
            }
            ViewData["EmployeeNumber"] = new SelectList(_context.Employees, "EmployeeNumber", "EmployeeNumber", timesheets.EmployeeNumber);
            return View(timesheets);
        }

        // POST: Timesheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeNumber,EndDate")] Timesheets timesheets)
        {
            if (id != timesheets.EmployeeNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timesheets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimesheetsExists(timesheets.EmployeeNumber))
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
            ViewData["EmployeeNumber"] = new SelectList(_context.Employees, "EmployeeNumber", "EmployeeNumber", timesheets.EmployeeNumber);
            return View(timesheets);
        }

        // GET: Timesheets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheets = await _context.Timesheets
                .Include(t => t.Employee)
                .SingleOrDefaultAsync(m => m.EmployeeNumber == id);
            if (timesheets == null)
            {
                return NotFound();
            }

            return View(timesheets);
        }

        // POST: Timesheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var timesheets = await _context.Timesheets.SingleOrDefaultAsync(m => m.EmployeeNumber == id);
            _context.Timesheets.Remove(timesheets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimesheetsExists(string id)
        {
            return _context.Timesheets.Any(e => e.EmployeeNumber == id);
        }
    }
}
