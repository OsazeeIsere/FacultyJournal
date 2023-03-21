using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FacultyJournal.Data;
using FacultyJournal.Models;
using System.Security.Claims;

namespace FacultyJournal.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticlesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> DownloadArticle(string? id)
        {
            var article = await _context.Articles.FindAsync(id);
            if(article != null)
            {
                string FileName = article.UploadedFile;
                string path = Path.Combine(this._hostingEnvironment.WebRootPath, "Articles/") + FileName;
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                return File(bytes, "application/octet-stream", FileName);
            }
            return NotFound();
        }
        // Creating a modal form to display abstact
        public async Task<IActionResult> Abstract(int? id)
        {
            var article = await _context.Articles.FindAsync(id);
            ViewBag.Abstract = article.Abstract;
            return PartialView("_Abstract");
        }
        // GET: Articles
        public async Task<IActionResult> Index()
        {

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId);
            ViewBag.user = user.Email;
            return View(await _context.Articles.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.ManuscriptTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, Article article)
        {
            var articles = await _context.Articles.FindAsync(id);

            
            if (articles != null)
            {
                try
                {
                     articles.Status = article.Status;
                     await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {

                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            
            return View(article);

        }


        // GET: Articles/Create
        public async Task<IActionResult> Create()
        {
            // Creat a list of Manuscript Type
            List<ManuscriptType> manuscriptType = new List<ManuscriptType>();
            manuscriptType = (from c in _context.ManuscriptTypes select c).ToList();
            ViewBag.message = manuscriptType;
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId);
            ViewBag.userId = currentUserId;
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Abstract,Keywords,ManuscriptTypeId,NumberOfPages,DateSubmited,SubmittedBy")] Article article)
        {

            _context.Add(article);
            await _context.SaveChangesAsync();
            //var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("student2@student.com", "student2@student.com"));
            //message.To.Add(new MailboxAddress("Osazee Isere", "osazee.isere@physci.uniben.edu"));
            //message.Body = new TextPart("plain")
            //{
            //    Text = "Article with the following Title:" + article.Title + "has been submitted"
            //};
            //using (var client = new SmtpClient())
            //{
            //    client.Connect("smtp.gmail.com", 587, false);
            //    client.Authenticate("student2@student.com", "Student2@student.com");
            //    client.Send(message);
            //    client.Disconnect(true);

            //}
            // Create the SmtpClient object
            return RedirectToAction(nameof(Index));
            //return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Abstract,Keywords,NumberOfPages,DateSubmited,SubmittedBy")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

              try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Articles'  is null.");
            }
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> UploadArticles(int? id)
        {

            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadArticles(int id, IFormFile articleUpload, Article article)
        {
            var articleToUpdate = await _context.Articles.FindAsync(article.Id);

            if (id != article.Id)
            {
                return NotFound();
            }

            try
            {
                if (articleUpload != null && articleUpload.Length > 0)
                {
                    var uploadDir = @"Articles";
                    var fileName = Path.GetFileNameWithoutExtension(articleUpload.FileName);
                    var extension = Path.GetExtension(articleUpload.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        articleUpload.CopyTo(fs);
                        articleToUpdate.UploadedFile = fileName;

                    }

                }
                await TryUpdateModelAsync<Article>(articleToUpdate);               //    c => c.Ssce1, c => c.Ssce2, c => c.BirthCertificate, c => c.DirectEntryUpload, c => c.LGAUpload))

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {

                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(article.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        return View(article);
        }

    }
}
