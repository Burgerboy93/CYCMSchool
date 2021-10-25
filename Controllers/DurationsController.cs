using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CYCMSchool.Data;
using CYCMSchool.Models;

namespace CYCMSchool.Controllers
{
    public class DurationsController : Controller
    {
        private readonly SchoolContext _context;

        public DurationsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Durations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Durations.ToListAsync());
        }

        // GET: Durations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duration = await _context.Durations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duration == null)
            {
                return NotFound();
            }

            return View(duration);
        }

        // GET: Durations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Durations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DurationTime,Cost")] Duration duration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(duration);
        }

        // GET: Durations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duration = await _context.Durations.FindAsync(id);
            if (duration == null)
            {
                return NotFound();
            }
            return View(duration);
        }

        // POST: Durations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DurationTime,Cost")] Duration duration)
        {
            if (id != duration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DurationExists(duration.Id))
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
            return View(duration);
        }

        // GET: Durations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duration = await _context.Durations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duration == null)
            {
                return NotFound();
            }

            return View(duration);
        }

        // POST: Durations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var duration = await _context.Durations.FindAsync(id);
            _context.Durations.Remove(duration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DurationExists(int id)
        {
            return _context.Durations.Any(e => e.Id == id);
        }
    }
}
