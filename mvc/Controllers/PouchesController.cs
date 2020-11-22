using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class PouchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PouchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pouches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pouch.ToListAsync());
        }

        // GET: Pouches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pouch = await _context.Pouch
                .FirstOrDefaultAsync(m => m.PouchID == id);
            if (pouch == null)
            {
                return NotFound();
            }

            return View(pouch);
        }

        // GET: Pouches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pouches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PouchID,Name,Description,Amount,Weight,UsagePerDay,StockOut")] Pouch pouch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pouch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pouch);
        }

        // GET: Pouches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pouch = await _context.Pouch.FindAsync(id);
            if (pouch == null)
            {
                return NotFound();
            }
            return View(pouch);
        }

        // POST: Pouches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PouchID,Name,Description,Amount,Weight,UsagePerDay,StockOut")] Pouch pouch)
        {
            if (id != pouch.PouchID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pouch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PouchExists(pouch.PouchID))
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
            return View(pouch);
        }

        // GET: Pouches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pouch = await _context.Pouch
                .FirstOrDefaultAsync(m => m.PouchID == id);
            if (pouch == null)
            {
                return NotFound();
            }

            return View(pouch);
        }

        // POST: Pouches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pouch = await _context.Pouch.FindAsync(id);
            _context.Pouch.Remove(pouch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PouchExists(int id)
        {
            return _context.Pouch.Any(e => e.PouchID == id);
        }
    }
}
