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
    public class LaborGradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LaborGradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LaborGrades
        public async Task<IActionResult> Index()
        {
            return View(await _context.LaborGrades.ToListAsync());
        }

        // GET: LaborGrades/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laborGrades = await _context.LaborGrades
                .SingleOrDefaultAsync(m => m.Grade == id);
            if (laborGrades == null)
            {
                return NotFound();
            }

            return View(laborGrades);
        }

        // GET: LaborGrades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LaborGrades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Grade,PayAmount")] LaborGrades laborGrades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laborGrades);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(laborGrades);
        }

        // GET: LaborGrades/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laborGrades = await _context.LaborGrades.SingleOrDefaultAsync(m => m.Grade == id);
            if (laborGrades == null)
            {
                return NotFound();
            }
            return View(laborGrades);
        }

        // POST: LaborGrades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Grade,PayAmount")] LaborGrades laborGrades)
        {
            if (id != laborGrades.Grade)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laborGrades);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaborGradesExists(laborGrades.Grade))
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
            return View(laborGrades);
        }

        // GET: LaborGrades/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laborGrades = await _context.LaborGrades
                .SingleOrDefaultAsync(m => m.Grade == id);
            if (laborGrades == null)
            {
                return NotFound();
            }

            return View(laborGrades);
        }

        // POST: LaborGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var laborGrades = await _context.LaborGrades.SingleOrDefaultAsync(m => m.Grade == id);
            _context.LaborGrades.Remove(laborGrades);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaborGradesExists(string id)
        {
            return _context.LaborGrades.Any(e => e.Grade == id);
        }
    }
}
