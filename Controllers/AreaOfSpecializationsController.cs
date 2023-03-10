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
    public class AreaOfSpecializationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AreaOfSpecializationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AreaOfSpecializations
        public async Task<IActionResult> Index()
        {
              return View(await _context.AreasOfSpecialization.ToListAsync());
        }

        // GET: AreaOfSpecializations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AreasOfSpecialization == null)
            {
                return NotFound();
            }

            var areaOfSpecialization = await _context.AreasOfSpecialization
                .FirstOrDefaultAsync(m => m.Id == id);
            if (areaOfSpecialization == null)
            {
                return NotFound();
            }

            return View(areaOfSpecialization);
        }

        // GET: AreaOfSpecializations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AreaOfSpecializations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Area")] AreaOfSpecialization areaOfSpecialization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(areaOfSpecialization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(areaOfSpecialization);
        }

        // GET: AreaOfSpecializations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AreasOfSpecialization == null)
            {
                return NotFound();
            }

            var areaOfSpecialization = await _context.AreasOfSpecialization.FindAsync(id);
            if (areaOfSpecialization == null)
            {
                return NotFound();
            }
            return View(areaOfSpecialization);
        }

        // POST: AreaOfSpecializations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Area")] AreaOfSpecialization areaOfSpecialization)
        {
            if (id != areaOfSpecialization.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(areaOfSpecialization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaOfSpecializationExists(areaOfSpecialization.Id))
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
            return View(areaOfSpecialization);
        }

        // GET: AreaOfSpecializations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AreasOfSpecialization == null)
            {
                return NotFound();
            }

            var areaOfSpecialization = await _context.AreasOfSpecialization
                .FirstOrDefaultAsync(m => m.Id == id);
            if (areaOfSpecialization == null)
            {
                return NotFound();
            }

            return View(areaOfSpecialization);
        }

        // POST: AreaOfSpecializations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AreasOfSpecialization == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AreasOfSpecialization'  is null.");
            }
            var areaOfSpecialization = await _context.AreasOfSpecialization.FindAsync(id);
            if (areaOfSpecialization != null)
            {
                _context.AreasOfSpecialization.Remove(areaOfSpecialization);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaOfSpecializationExists(int id)
        {
          return _context.AreasOfSpecialization.Any(e => e.Id == id);
        }
    }
}
