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
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string search, int? id, int? lessonId, int? letterId)
        {
            
            var viewModel = new StudentIndexData();
            viewModel.StudentID = id ?? 0;
            viewModel.Students = await _context.Students
                .AsNoTracking()
                .Include(i => i.Lessons)
                    .ThenInclude(i => i.Letters)
                .Include(i => i.Lessons)
                    .ThenInclude(i => i.Tutor)
                .Include(i => i.Lessons)
                    .ThenInclude(i => i.Instrument)
                .Include(i => i.Lessons)
                    .ThenInclude(i => i.Term)
                .Include(i => i.Letters)
                    .ThenInclude(i => i.Bank)
                .Where(i => i.Inactive.Equals(false))
                .ToListAsync();

            
            if (id != null)
            {
                ViewData["StudentID"] = id.Value;
                Student student = viewModel.Students.Where(
                    i => i.Id == id.Value).Single();
                viewModel.Lessons = student.Lessons;
            }

            if (id != null)
            {
                ViewData["StudentID"] = id.Value;
                Student student = viewModel.Students.Where(
                    i => i.Id == id.Value).Single();
                viewModel.Letters = student.Letters;
            }


            return View(viewModel);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(l => l.Lessons)
                    .ThenInclude(s => s.Tutor)
                .Include(l => l.Lessons)
                    .ThenInclude(s => s.Instrument)
                .Include(l => l.Lessons)
                    .ThenInclude(s => s.Duration)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateOfBirth,StudentGender,GuardianName,ContactEmail,ContactNumber,Inactive")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth,StudentGender,GuardianName,ContactEmail,ContactNumber,Inactive")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public Task<IActionResult> LetterCreate(int id)
        //{
        //    var letter = new Letter();
        //    letter.Lessons = new List<Lesson>();
        //    PopulateAssignedLessonsList(letter, id);
        //    ViewData["BankID"] = new SelectList(_context.Banks, "Id", "AccountName");
        //    return View(student);
        //}


        private void PopulateAssignedLessonsList(Letter letter, int studentId, bool paid = false)
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

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        private void PopulateGenderList(Student students)
        {

        }
    }
}
