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
    public class WorkPackagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkPackagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkPackages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WorkPackages.Include(w => w.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WorkPackages/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workPackages = await _context.WorkPackages
                .Include(w => w.Project)
                .SingleOrDefaultAsync(m => m.ProjectNumber == id);
            if (workPackages == null)
            {
                return NotFound();
            }

            return View(workPackages);
        }

        // GET: WorkPackages/Create
        public IActionResult Create()
        {
            ViewData["ProjectNumber"] = new SelectList(_context.Projects, "ProjectNumber", "ProjectNumber");
            return View();
        }

        // POST: WorkPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectNumber,WorkPackageNumber,Description")] WorkPackages workPackages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workPackages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectNumber"] = new SelectList(_context.Projects, "ProjectNumber", "ProjectNumber", workPackages.ProjectNumber);
            return View(workPackages);
        }

        // GET: WorkPackages/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workPackages = await _context.WorkPackages.SingleOrDefaultAsync(m => m.ProjectNumber == id);
            if (workPackages == null)
            {
                return NotFound();
            }
            ViewData["ProjectNumber"] = new SelectList(_context.Projects, "ProjectNumber", "ProjectNumber", workPackages.ProjectNumber);
            return View(workPackages);
        }

        // POST: WorkPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProjectNumber,WorkPackageNumber,Description")] WorkPackages workPackages)
        {
            if (id != workPackages.ProjectNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workPackages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkPackagesExists(workPackages.ProjectNumber))
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
            ViewData["ProjectNumber"] = new SelectList(_context.Projects, "ProjectNumber", "ProjectNumber", workPackages.ProjectNumber);
            return View(workPackages);
        }

        // GET: WorkPackages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workPackages = await _context.WorkPackages
                .Include(w => w.Project)
                .SingleOrDefaultAsync(m => m.ProjectNumber == id);
            if (workPackages == null)
            {
                return NotFound();
            }

            return View(workPackages);
        }

        // POST: WorkPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var workPackages = await _context.WorkPackages.SingleOrDefaultAsync(m => m.ProjectNumber == id);
            _context.WorkPackages.Remove(workPackages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkPackagesExists(string id)
        {
            return _context.WorkPackages.Any(e => e.ProjectNumber == id);
        }
    }
}
