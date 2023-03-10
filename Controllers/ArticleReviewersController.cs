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
    public class ArticleReviewersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticleReviewersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArticleReviewers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ArticleReviewers.Include(a => a.Reviewers);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ArticleReviewers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ArticleReviewers == null)
            {
                return NotFound();
            }

            var articleReviewer = await _context.ArticleReviewers
                .Include(a => a.Reviewers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articleReviewer == null)
            {
                return NotFound();
            }

            return View(articleReviewer);
        }

        // GET: ArticleReviewers/Create
        public IActionResult Create()
        {
            ViewData["ReviewerEmail"] = new SelectList(_context.Reviewers, "Email", "Email");
            return View();
        }

        // POST: ArticleReviewers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArticleId,Title,ReviewerEmail,ReviewerId,DateAssigned")] ArticleReviewer articleReviewer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articleReviewer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReviewerEmail"] = new SelectList(_context.Reviewers, "Email", "Email", articleReviewer.ReviewerEmail);
            return View(articleReviewer);
        }

        // GET: ArticleReviewers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ArticleReviewers == null)
            {
                return NotFound();
            }

            var articleReviewer = await _context.ArticleReviewers.FindAsync(id);
            if (articleReviewer == null)
            {
                return NotFound();
            }
            ViewData["ReviewerEmail"] = new SelectList(_context.Reviewers, "Email", "Email", articleReviewer.ReviewerEmail);
            return View(articleReviewer);
        }

        // POST: ArticleReviewers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArticleId,Title,ReviewerEmail,ReviewerId,DateAssigned")] ArticleReviewer articleReviewer)
        {
            if (id != articleReviewer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articleReviewer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleReviewerExists(articleReviewer.Id))
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
            ViewData["ReviewerEmail"] = new SelectList(_context.Reviewers, "Email", "Email", articleReviewer.ReviewerEmail);
            return View(articleReviewer);
        }

        // GET: ArticleReviewers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ArticleReviewers == null)
            {
                return NotFound();
            }

            var articleReviewer = await _context.ArticleReviewers
                .Include(a => a.Reviewers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articleReviewer == null)
            {
                return NotFound();
            }

            return View(articleReviewer);
        }

        // POST: ArticleReviewers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ArticleReviewers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ArticleReviewers'  is null.");
            }
            var articleReviewer = await _context.ArticleReviewers.FindAsync(id);
            if (articleReviewer != null)
            {
                _context.ArticleReviewers.Remove(articleReviewer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleReviewerExists(int id)
        {
          return _context.ArticleReviewers.Any(e => e.Id == id);
        }
    }
}
