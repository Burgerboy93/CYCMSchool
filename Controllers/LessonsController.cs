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

namespace CYCMSchool.Controllers
{
    public class LessonsController : Controller
    {
        private readonly SchoolContext _context;

        public LessonsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index(string search,int? id)
        {
            var viewModel = new LessonsIndexData();
            ViewData["CurrentFilter"] = search;
            viewModel.Lessons = await _context.Lessons
                .Include(i => i.Student)
                .Include(i => i.Instrument)
                .Include(i => i.Tutor)
                .Include(i => i.Term)
                .Include(i => i.Letters)
                    .ThenInclude(i => i.Bank)
                .Include(i => i.Letters)
                    .ThenInclude(i => i.Student)
                .Include(i => i.Duration)
                .AsNoTracking()
                .ToListAsync();

            //Get search results
            if (!String.IsNullOrEmpty(search))
            {

                viewModel.Lessons = _context.Lessons
                .AsNoTracking()
                .Include(i => i.Student)
                .Include(i => i.Instrument)
                .Include(i => i.Tutor)
                .Include(i => i.Term)
                .Include(i => i.Letters)
                    .ThenInclude(i => i.Bank)
                .Where(i => i.Student.FirstName.Contains(search) && i.Student.Inactive.Equals(false) 
                || i.Student.LastName.Contains(search) && i.Student.Inactive.Equals(false))
                .ToList();

                return View(viewModel);
            }

            if (id != null)
            {
                ViewData["LessonID"] = id.Value;
                Lesson lesson = viewModel.Lessons.Where(
                    i => i.Id == id.Value).Single();
                viewModel.Letters = lesson.Letters;
            }
            return View(viewModel);
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Duration)
                .Include(l => l.Instrument)
                .Include(l => l.Student)
                .Include(l => l.Term)
                .Include(l => l.Tutor)
                .Include(l => l.Letters)
                    .ThenInclude(l => l.Bank)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create(int? studentID)
        {
            ViewData["DurationID"] = new SelectList(_context.Durations, "Id", "DurationTime");
            ViewData["InstrumentID"] = new SelectList(_context.Instruments, "Id", "InstrumentName");
            ViewData["StudentID"] = new SelectList(_context.Students, "Id", "FullName");
            ViewData["TutorID"] = new SelectList(_context.Tutors, "Id", "FullName");
            ViewData["TermID"] = new SelectList(_context.Terms, "Id", "TermNumber");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LessonDate,StudentID,TutorID,InstrumentID,DurationID,TermID,Paid")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DurationID"] = new SelectList(_context.Durations, "Id", "DurationTime", lesson.DurationID);
            ViewData["InstrumentID"] = new SelectList(_context.Instruments, "Id", "InstrumentName", lesson.InstrumentID);
            ViewData["StudentID"] = new SelectList(_context.Students, "Id", "FullName", lesson.StudentID);
            ViewData["TutorID"] = new SelectList(_context.Tutors, "Id", "FullName", lesson.TutorID);
            ViewData["TermID"] = new SelectList(_context.Terms, "Id", "TermNumber", lesson.TermID);
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["DurationID"] = new SelectList(_context.Durations, "Id", "DurationTime", lesson.DurationID);
            ViewData["InstrumentID"] = new SelectList(_context.Instruments, "Id", "InstrumentName", lesson.InstrumentID);
            ViewData["StudentID"] = new SelectList(_context.Students, "Id", "FullName", lesson.StudentID);
            ViewData["TutorID"] = new SelectList(_context.Tutors, "Id", "FullName", lesson.TutorID);
            ViewData["TermID"] = new SelectList(_context.Terms, "Id", "TermNumber");
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LessonDate,StudentID,TutorID,InstrumentID,TermID,DurationID,Paid")] Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.Id))
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
            ViewData["DurationID"] = new SelectList(_context.Durations, "Id", "DurationTime", lesson.DurationID);
            ViewData["InstrumentID"] = new SelectList(_context.Instruments, "Id", "InstrumentName", lesson.InstrumentID);
            ViewData["StudentID"] = new SelectList(_context.Students, "Id", "FullName", lesson.StudentID);
            ViewData["TutorID"] = new SelectList(_context.Tutors, "Id", "FullName", lesson.TutorID);
            ViewData["TermID"] = new SelectList(_context.Terms, "Id", "TermNumber");
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Duration)
                .Include(l => l.Instrument)
                .Include(l => l.Student)
                .Include(l => l.Tutor)
                .Include(l => l.Term)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }
    }
}
