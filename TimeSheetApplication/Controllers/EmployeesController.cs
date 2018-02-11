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
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Employees.Include(e => e.AuthorizationCode).Include(e => e.LaborGrade);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.AuthorizationCode)
                .Include(e => e.LaborGrade)
                .SingleOrDefaultAsync(m => m.EmployeeNumber == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["AuthCode"] = new SelectList(_context.AuthorizationCodes, "AuthCode", "AuthCode");
            ViewData["Grade"] = new SelectList(_context.LaborGrades, "Grade", "Grade");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeNumber,FirstName,LastName,Grade,EmployeeIntials,SupervisorNumber,AuthCode")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthCode"] = new SelectList(_context.AuthorizationCodes, "AuthCode", "AuthCode", employees.AuthCode);
            ViewData["Grade"] = new SelectList(_context.LaborGrades, "Grade", "Grade", employees.Grade);
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeNumber == id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["AuthCode"] = new SelectList(_context.AuthorizationCodes, "AuthCode", "AuthCode", employees.AuthCode);
            ViewData["Grade"] = new SelectList(_context.LaborGrades, "Grade", "Grade", employees.Grade);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeNumber,FirstName,LastName,Grade,EmployeeIntials,SupervisorNumber,AuthCode")] Employees employees)
        {
            if (id != employees.EmployeeNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.EmployeeNumber))
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
            ViewData["AuthCode"] = new SelectList(_context.AuthorizationCodes, "AuthCode", "AuthCode", employees.AuthCode);
            ViewData["Grade"] = new SelectList(_context.LaborGrades, "Grade", "Grade", employees.Grade);
            return View(employees);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.AuthorizationCode)
                .Include(e => e.LaborGrade)
                .SingleOrDefaultAsync(m => m.EmployeeNumber == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employees = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeNumber == id);
            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(string id)
        {
            return _context.Employees.Any(e => e.EmployeeNumber == id);
        }
    }
}
