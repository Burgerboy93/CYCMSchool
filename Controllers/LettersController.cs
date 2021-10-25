using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CYCMSchool.Data;
using CYCMSchool.Models;
using CYCMSchool.Models.ViewModels;
using PagedList;
using System.Net.Mail;
using System.Text;

namespace CYCMSchool.Controllers
{
    public class LettersController : Controller
    {
        private readonly SchoolContext _context;

        public List<AssignedLessonsData> AssignedLessonsDataList { get; private set; }

        public LettersController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var viewModel = new LettersIndexData();
            viewModel.Letters = await _context.Letters
                .Include(i => i.Lessons)
                    .ThenInclude(i => i.Student)
                .Include(i => i.Lessons)
                    .ThenInclude(i => i.Tutor)
                .Include(i => i.Lessons)
                    .ThenInclude(i => i.Instrument)
                .Include(i => i.Lessons)
                    .ThenInclude(i => i.Duration)
                .Include(i => i.Lessons)
                    .ThenInclude(i => i.Term)
                .Include(i => i.Bank)
                .Include(i => i.EmailSignature)
                .Include(i => i.Student)
                .AsNoTracking()
                .ToListAsync();

            
            if (id != null)
            {
                ViewData["LetterID"] = id.Value;
                Letter letter = viewModel.Letters
                    .Where(i => i.Id == id.Value)
                    .Single();
                viewModel.Lessons = letter.Lessons;
            }

            return View(viewModel);
        }

        //GET Details
        public async Task<IActionResult> Details(int? id)
        {


            var letter = await _context.Letters
                .AsNoTracking()
                .Include(m => m.Bank)
                .Include(m => m.EmailSignature)
                .Include(m => m.Student)
                .Include(m => m.Lessons)
                .Include(m => m.Lessons)
                    .ThenInclude(m => m.Student)
                .Include(m => m.Lessons)
                    .ThenInclude(m => m.Term)
                .Include(m => m.Lessons)
                    .ThenInclude(m => m.Tutor)
                .Include(m => m.Lessons)
                    .ThenInclude(m => m.Instrument)
                .Include(m => m.Lessons)
                    .ThenInclude(m => m.Duration)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (letter == null)
            {
                return NotFound();
            }

            return View(letter);
        }
        // GET: Letters/Create
        public IActionResult Create(int? studentID)
        {

            if (studentID == null)
            {
                return View();
            }

            var letter = new Letter();
            letter.Lessons = new List<Lesson>();
            PopulateAssignedLessonsList(letter, studentID);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName");
            ViewData["BankID"] = new SelectList(_context.Banks, "Id", "AccountName");
            ViewData["EmailSignatureID"] = new SelectList(_context.EmailSignatures, "Id", "Signature");
            return View();
        }

        // POST: Letters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comment,BankID,StudentId,PaymentDate,EmailSignatureId")] Letter letter, int [] selectedLessons)
        {
            if(selectedLessons != null)
            {
                if (ModelState.IsValid)
                {
                    letter.Lessons = new List<Lesson>();
                    foreach (var lesson in selectedLessons)
                    {
                        var lessonToAdd = new Lesson { Id = lesson };
                        letter.Lessons.Add(lessonToAdd);
                        _context.Attach(lessonToAdd);
                    }


                    _context.Add(letter);
                    await _context.SaveChangesAsync();
                    var emailLetter = await _context.Letters
                                    .AsNoTracking()
                                    .Include(m => m.Bank)
                                    .Include(m => m.EmailSignature)
                                    .Include(m => m.Student)
                                    .Include(m => m.Lessons)
                                    .Include(m => m.Lessons)
                                        .ThenInclude(m => m.Student)
                                    .Include(m => m.Lessons)
                                        .ThenInclude(m => m.Term)
                                    .Include(m => m.Lessons)
                                        .ThenInclude(m => m.Tutor)
                                    .Include(m => m.Lessons)
                                        .ThenInclude(m => m.Instrument)
                                    .Include(m => m.Lessons)
                                        .ThenInclude(m => m.Duration)
                                    .FirstOrDefaultAsync(m => m.Id == letter.Id);
                    SendLetter(emailLetter);
                    return RedirectToAction(nameof(Index));
                }
                }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", letter.StudentId);
            ViewData["BankID"] = new SelectList(_context.Banks, "Id", "AccountName", letter.BankID);
            ViewData["EmailSignatureId"] = new SelectList(_context.EmailSignatures, "Id", "Signature", letter.EmailSignatureId);
            return View("~/Views/Students/Index.cshtml");
        }

        private void SendLetter(Letter letter)
        {

            MailMessage mail = new MailMessage();
            mail.To.Add(letter.Student.ContactEmail);
            mail.From = new MailAddress("S276527.HIT339@gmail.com");
            mail.Subject = letter.LetterRef + " - Invoice";
            mail.Body = BuildBody(letter);
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("", "");
            smtp.EnableSsl = true;
            smtp.Send(mail);
            
        }
        private bool LetterExists(int id)
        {
            return _context.Letters.Any(e => e.Id == id);
        }

        private string BuildBody(Letter letter)
        {
            decimal total = 0;
            int termNumber = 0;
            int semester = 0;
            DateTime start = DateTime.Now;

            var body = new StringBuilder();

            foreach(var lesson in letter.Lessons)
            {
                total += lesson.Duration.Cost;

                if (termNumber != lesson.Term.TermNumber)
                {
                    termNumber = lesson.Term.TermNumber;
                    if(termNumber == 1 || termNumber == 2)
                    {
                        semester = 1;
                    }
                    else
                    {
                        semester = 2;
                    }
                    start = lesson.Term.TermStart;
                }
            }

            //greetings bit
            body.AppendLine($"Dear, {letter.Student.FirstName}");
            body.AppendLine("<br><br>");
            body.AppendLine(letter.Comment);
            body.AppendLine("<br><br>");
            body.AppendLine($"Welcome to all existing and new students. Term {termNumber} will commence {start.ToShortDateString()}.");
            body.AppendLine($"Please ensure your payment is finalised by {letter.PaymentDate.ToShortDateString()}. If a student is no longer attending lessons, please email the CYCM to be removed from the email list.");
            body.AppendLine("<br><br>");
            body.AppendLine("If paying by Bank Transfer-EFT, please forward a copy of your payment to the office, to follow up and allocate to the student.");
            body.AppendLine("<br><br>");
            body.AppendLine("<b>PAYMENT DETAILS:</b>");
            body.AppendLine("<br><br>");
            body.AppendLine($"Ref#: {letter.LetterRef}");
            body.AppendLine("<br><br>");
            body.AppendLine($"Student: {letter.Student.FullName}");
            body.AppendLine("<br><br>");
            body.AppendLine($"Amount: {total}");
            body.AppendLine("<br><br>");
            body.AppendLine($"<b>Please follow the <a href='https://webpay.cdu.edu.au/musicschool/tran?tran-type=006&REFNO={letter.LetterRef}&CUSTNAME={letter.Student.LastName}_{letter.Student.FirstName}&PARENTSNAME={letter.Student.GuardianName}&UNITAMOUNTINCTAX={total}'>link</a> to make payment for {termNumber}, {start.Year}.</b>");
            body.AppendLine("<br><br>");
            body.AppendLine($"<b>Apply for your Sport Vouchers for semester {semester}, of {start.Year}, please visit the <a href='https://sportvoucher.nt.gov.au'>Sport Voucher website</a>, as schools are no longer providing students with a sport voucher.</b>");
            body.AppendLine("<br><br>");
            body.AppendLine("Alternatively pay via Bank Transfer-EFT - CDU bank details below: ");
            body.AppendLine("<br><br>");
            body.AppendLine($"<b>Bank: {letter.Bank.BankName}</b>");
            body.AppendLine("<br><br>");
            body.AppendLine($"<b>Account Name: {letter.Bank.AccountName}</b>");
            body.AppendLine("<br><br>");
            body.AppendLine($"<b>BSB Number: {letter.Bank.BSB}</b>");
            body.AppendLine("<br><br>");
            body.AppendLine($"<b>Account Number: {letter.Bank.AccountNumber}</b>");
            body.AppendLine("<br><br>");
            body.AppendLine("<b>Reference number - please include 'CYCM, Reference number and student name</b>");
            body.AppendLine("<br><br>");
            body.AppendLine("<i>The CYCM is committed to providing students with quality lessons in a positive learning environment.</i>");
            body.AppendLine("<br><br>");
            body.AppendLine("<i>Regards</i>");
            body.AppendLine("<br><br>");
            body.AppendLine($"{letter.EmailSignature.Signature}");


            return body.ToString();
        }

        private void PopulateAssignedLessonsList(Letter letter, int? studentId, bool paid = false)
        {

            var allLessons = _context.Lessons.Where(l => l.Paid == paid && l.StudentID == studentId);
            var letterLessons = new HashSet<int>(
                letter.Lessons.Select(l => l.Id));
            var AssignedLessonsDataList = new List<AssignedLessonsData>();
            foreach (var lesson in allLessons)
            {
                AssignedLessonsDataList.Add(new AssignedLessonsData
                {
                    LessonID = lesson.Id,
                    LessonDate = lesson.LessonDate,
                    Selected = letterLessons.Contains(lesson.Id)
                });
            }
            ViewData["Lessons"] = AssignedLessonsDataList;
        }

    }
}
