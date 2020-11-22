using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.Views.Refuels
{
    public class RefuelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RefuelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Refuels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Refuel.Include(r => r.Vehicle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Refuels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refuel = await _context.Refuel
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.RefuelID == id);
            if (refuel == null)
            {
                return NotFound();
            }

            return View(refuel);
        }

        // GET: Refuels/Create
        public IActionResult Create()
        {
            ViewData["VehicleID"] = new SelectList(_context.Vehicle, "ID", "Name");
            return View();
        }

        // POST: Refuels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefuelID,VehicleID,RefuelDate,Amount,Price")] Refuel refuel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refuel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleID"] = new SelectList(_context.Vehicle, "ID", "Name", refuel.VehicleID);
            return View(refuel);
        }

        // GET: Refuels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refuel = await _context.Refuel.FindAsync(id);
            if (refuel == null)
            {
                return NotFound();
            }
            ViewData["VehicleID"] = new SelectList(_context.Vehicle, "ID", "Name", refuel.VehicleID);
            return View(refuel);
        }

        // POST: Refuels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefuelID,VehicleID,RefuelDate,Amount,Price")] Refuel refuel)
        {
            if (id != refuel.RefuelID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refuel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefuelExists(refuel.RefuelID))
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
            ViewData["VehicleID"] = new SelectList(_context.Vehicle, "ID", "Name", refuel.VehicleID);
            return View(refuel);
        }

        // GET: Refuels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refuel = await _context.Refuel
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.RefuelID == id);
            if (refuel == null)
            {
                return NotFound();
            }

            return View(refuel);
        }

        // POST: Refuels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refuel = await _context.Refuel.FindAsync(id);
            _context.Refuel.Remove(refuel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RefuelExists(int id)
        {
            return _context.Refuel.Any(e => e.RefuelID == id);
        }
    }
}
