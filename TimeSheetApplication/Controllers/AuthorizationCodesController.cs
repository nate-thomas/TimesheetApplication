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
    public class AuthorizationCodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorizationCodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AuthorizationCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AuthorizationCodes.ToListAsync());
        }

        // GET: AuthorizationCodes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorizationCodes = await _context.AuthorizationCodes
                .SingleOrDefaultAsync(m => m.AuthCode == id);
            if (authorizationCodes == null)
            {
                return NotFound();
            }

            return View(authorizationCodes);
        }

        // GET: AuthorizationCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuthorizationCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthCode")] AuthorizationCodes authorizationCodes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorizationCodes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authorizationCodes);
        }

        // GET: AuthorizationCodes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorizationCodes = await _context.AuthorizationCodes.SingleOrDefaultAsync(m => m.AuthCode == id);
            if (authorizationCodes == null)
            {
                return NotFound();
            }
            return View(authorizationCodes);
        }

        // POST: AuthorizationCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AuthCode")] AuthorizationCodes authorizationCodes)
        {
            if (id != authorizationCodes.AuthCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorizationCodes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorizationCodesExists(authorizationCodes.AuthCode))
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
            return View(authorizationCodes);
        }

        // GET: AuthorizationCodes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorizationCodes = await _context.AuthorizationCodes
                .SingleOrDefaultAsync(m => m.AuthCode == id);
            if (authorizationCodes == null)
            {
                return NotFound();
            }

            return View(authorizationCodes);
        }

        // POST: AuthorizationCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var authorizationCodes = await _context.AuthorizationCodes.SingleOrDefaultAsync(m => m.AuthCode == id);
            _context.AuthorizationCodes.Remove(authorizationCodes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorizationCodesExists(string id)
        {
            return _context.AuthorizationCodes.Any(e => e.AuthCode == id);
        }
    }
}
