using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FacultyJournal.Data;
using FacultyJournal.Models;
//using MailKit.Net.Smtp;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Net;
namespace FacultyJournal.Controllers
{
    public class ReviewersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SendMail()
        {
            ViewData["AreaOfSpecializationId"] = new SelectList(_context.AreasOfSpecialization, "Id", "Area");
            return View();

            //var applicationDbContext = _context.Reviewers.Include(r => r.AreasOfSpecialization);
            //return View(await applicationDbContext.ToListAsync());
        }
        [AllowAnonymous]
           [HttpPost]
        public async Task<IActionResult> sendEMail()
        {
            // Create a new email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("andy.schowalter63@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("osazee.isere2@gmail.com"));

            BodyBuilder bb = new BodyBuilder();

            bb.TextBody = "I want to see it succeed";
            email.Body = bb.ToMessageBody();
            // email.Body = new TextPart(TextFormat.Text) { Text = "I want to see it succeed" };
            email.Subject = "Test email from C#";
            // Sender's email address and password
            string fromEmail = "andy.schowalter63@ethereal.email";
            string password = "fVNfGfyGyV9GaVhxgJ";
            // string result = "successful";
            // Receiver's email address



            // create a new HttpWebRequest object
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://example.com");

            // set the timeout period to 30 seconds
            request.Timeout = 90000;

            try
            {


                // Create an instance of the SmtpClient class
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                  //  client.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                    client.Authenticate(fromEmail, password);
                    client.Send(email);
                    client.Disconnect(true);
                    client.Dispose();
                    // make the request and get the response
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    //Console.WriteLine("Email sent successfully.");
                    // Enable SSL and set the SMTP credentials

                    return RedirectToAction("notification", new { info = "E-mail sent successfully" });
                }
            }
            catch (Exception y)
            {
                string error = y.Message.ToString();
                return RedirectToAction("notification", new { info = error });
            }

            
        }

        // GET: Reviewers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reviewers.Include(r => r.AreasOfSpecialization);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reviewers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reviewers == null)
            {
                return NotFound();
            }

            var reviewer = await _context.Reviewers
                .Include(r => r.AreasOfSpecialization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviewer == null)
            {
                return NotFound();
            }

            return View(reviewer);
        }

        // GET: Reviewers/Create
        public IActionResult Create()
        {
            ViewData["AreaOfSpecializationId"] = new SelectList(_context.AreasOfSpecialization, "Id", "Area");
            return View();
        }

        // POST: Reviewers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,OtherName,LastName,Email,Affiliation,AreaOfSpecializationId")] Reviewer reviewer)
        {

            _context.Add(reviewer);
            await _context.SaveChangesAsync();
            // Create a new email message
            //var email = new MimeMessage();
            // Create a new email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("oz1.concepts@gmail.com"));
            email.To.Add(MailboxAddress.Parse("osazee.isere2@gmail.com"));

            BodyBuilder bb = new BodyBuilder();

            bb.TextBody = "I want to see it succeed";
            email.Body = bb.ToMessageBody();
            // email.Body = new TextPart(TextFormat.Text) { Text = "I want to see it succeed" };
            email.Subject = "Test email from C#";
            // Sender's email address and password
            string fromEmail = "oz1.concepts@gmail.com";
            string password = "prayer2JAH";
            // string result = "successful";
            // Receiver's email address



            // create a new HttpWebRequest object
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://example.com");

            // set the timeout period to 30 seconds
            request.Timeout = 90000;

            try
            {


                // Create an instance of the SmtpClient class
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.gmail.com", 465, SecureSocketOptions.StartTls);
                    client.Authenticate(fromEmail, password);


                    client.Send(email);
                    client.Disconnect(true);
                    client.Dispose();
                    // make the request and get the response
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    //Console.WriteLine("Email sent successfully.");
                    // Enable SSL and set the SMTP credentials

                    return RedirectToAction("notification", new { info = "E-mail sent successfully" });
                }
            }
            catch (Exception y)
            {
                string error = y.Message.ToString();
                return RedirectToAction("notification", new { info = error });
            }

            return Ok();
            return RedirectToAction(nameof(Index));

        }

        // GET: Reviewers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reviewers == null)
            {
                return NotFound();
            }

            var reviewer = await _context.Reviewers.FindAsync(id);
            if (reviewer == null)
            {
                return NotFound();
            }
            ViewData["AreaOfSpecializationId"] = new SelectList(_context.AreasOfSpecialization, "Id", "Id", reviewer.AreaOfSpecializationId);
            return View(reviewer);
        }

        // POST: Reviewers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,OtherName,LastName,Email,Affiliation,AreaOfSpecializationId,ArticleId")] Reviewer reviewer)
        {
            if (id != reviewer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviewer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewerExists(reviewer.Id))
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
            ViewData["AreaOfSpecializationId"] = new SelectList(_context.AreasOfSpecialization, "Id", "Id", reviewer.AreaOfSpecializationId);
            return View(reviewer);
        }

        // GET: Reviewers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reviewers == null)
            {
                return NotFound();
            }

            var reviewer = await _context.Reviewers
                .Include(r => r.AreasOfSpecialization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviewer == null)
            {
                return NotFound();
            }

            return View(reviewer);
        }

        // POST: Reviewers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reviewers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reviewers'  is null.");
            }
            var reviewer = await _context.Reviewers.FindAsync(id);
            if (reviewer != null)
            {
                _context.Reviewers.Remove(reviewer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewerExists(int id)
        {
            return _context.Reviewers.Any(e => e.Id == id);
        }

    }
}
