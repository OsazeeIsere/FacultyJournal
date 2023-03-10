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
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Authors.Include(a => a.Articles).Include(a => a.Countries);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .Include(a => a.Articles)
                .Include(a => a.Countries)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create(int? id)
        {
            int articleId = Convert.ToInt32(TempData["ArticleId"]);

            ViewBag.Id = id;
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Id");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // GET: Authors/Create
        public IActionResult AddAuthor()
        {
            int articleId = Convert.ToInt32(TempData["ArticleId"]);

            ViewBag.Id = articleId;
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Id");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }
        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,OtherName,LastName,Email,Affiliation,ORCIDNumber,IsCorrespondingAuthor,CountryId,ArticleId,MultipleAuthors")] Author author)
        {
            if (author.MultipleAuthors == true)
            {
                TempData["ArticleId"] = author.ArticleId;
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AddAuthor));
            }
            else
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Id", author.ArticleId);
            //ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", author.CountryId);
            //return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Countries, "Id", "Id", author.ArticleId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", author.CountryId);
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,OtherName,LastName,Email,Affiliation,IsCorrespondingAuthor,CountryId,ArticleId")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            ViewData["ArticleId"] = new SelectList(_context.Countries, "Id", "Id", author.ArticleId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", author.CountryId);
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .Include(a => a.Articles)
                .Include(a => a.Countries)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Authors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Authors'  is null.");
            }
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
