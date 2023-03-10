using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FacultyJournal.Data;
using FacultyJournal.Models;

namespace FacultyJournal.Controllers
{
    public class ManuscriptTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManuscriptTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManuscriptTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.ManuscriptTypes.ToListAsync());
        }

        // GET: ManuscriptTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ManuscriptTypes == null)
            {
                return NotFound();
            }

            var manuscriptType = await _context.ManuscriptTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manuscriptType == null)
            {
                return NotFound();
            }

            return View(manuscriptType);
        }

        // GET: ManuscriptTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManuscriptTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] ManuscriptType manuscriptType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manuscriptType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manuscriptType);
        }

        // GET: ManuscriptTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ManuscriptTypes == null)
            {
                return NotFound();
            }

            var manuscriptType = await _context.ManuscriptTypes.FindAsync(id);
            if (manuscriptType == null)
            {
                return NotFound();
            }
            return View(manuscriptType);
        }

        // POST: ManuscriptTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] ManuscriptType manuscriptType)
        {
            if (id != manuscriptType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manuscriptType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManuscriptTypeExists(manuscriptType.Id))
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
            return View(manuscriptType);
        }

        // GET: ManuscriptTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ManuscriptTypes == null)
            {
                return NotFound();
            }

            var manuscriptType = await _context.ManuscriptTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manuscriptType == null)
            {
                return NotFound();
            }

            return View(manuscriptType);
        }

        // POST: ManuscriptTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ManuscriptTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ManuscriptTypes'  is null.");
            }
            var manuscriptType = await _context.ManuscriptTypes.FindAsync(id);
            if (manuscriptType != null)
            {
                _context.ManuscriptTypes.Remove(manuscriptType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManuscriptTypeExists(int id)
        {
          return _context.ManuscriptTypes.Any(e => e.Id == id);
        }
    }
}
