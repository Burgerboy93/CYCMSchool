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
    public class EmailSignaturesController : Controller
    {
        private readonly SchoolContext _context;

        public EmailSignaturesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: EmailSignatures
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmailSignatures.ToListAsync());
        }

        // GET: EmailSignatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailSignature = await _context.EmailSignatures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emailSignature == null)
            {
                return NotFound();
            }

            return View(emailSignature);
        }

        // GET: EmailSignatures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmailSignatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Signature")] EmailSignature emailSignature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emailSignature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emailSignature);
        }

        // GET: EmailSignatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailSignature = await _context.EmailSignatures.FindAsync(id);
            if (emailSignature == null)
            {
                return NotFound();
            }
            return View(emailSignature);
        }

        // POST: EmailSignatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Signature")] EmailSignature emailSignature)
        {
            if (id != emailSignature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailSignature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailSignatureExists(emailSignature.Id))
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
            return View(emailSignature);
        }

        // GET: EmailSignatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailSignature = await _context.EmailSignatures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emailSignature == null)
            {
                return NotFound();
            }

            return View(emailSignature);
        }

        // POST: EmailSignatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emailSignature = await _context.EmailSignatures.FindAsync(id);
            _context.EmailSignatures.Remove(emailSignature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailSignatureExists(int id)
        {
            return _context.EmailSignatures.Any(e => e.Id == id);
        }
    }
}
