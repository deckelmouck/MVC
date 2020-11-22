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
    public class DialysisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DialysisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dialysis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Dialysis.Include(d => d.Pouch)
                .OrderBy(d => d.DialysisDate);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Dialysis Overview
        public async Task<IActionResult> Overview()
        {
            List<DailyDialysisViewModel> dailyDialysisViewModel = new List<DailyDialysisViewModel>();

            var data = await _context.Dialysis
                .Join(
                _context.Pouch,
                d => d.PouchID,
                p => p.PouchID,
                (d, p) => new { d.DialysisDate, d.OutWeight, p.Weight, Diff = 0 })
                .GroupBy(x => x.DialysisDate)
                .Select(y => new { DialysisDate = y.Key.Date, Outweight = y.Sum(o => o.OutWeight), Weight = y.Sum(g => g.Weight), Diff = y.Sum(h => h.OutWeight - h.Weight) })
                .ToListAsync();

            dailyDialysisViewModel = data.Select(a => new DailyDialysisViewModel
            {
                DialysisDate = a.DialysisDate,
                Outweight = a.Outweight,
                Weight = a.Weight,
                Diff = a.Diff
            })
            .ToList();

            return View(dailyDialysisViewModel);
        }


        // GET: Dialysis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dialysis = await _context.Dialysis
                .Include(d => d.Pouch)
                .FirstOrDefaultAsync(m => m.DialysisID == id);
            if (dialysis == null)
            {
                return NotFound();
            }

            return View(dialysis);
        }

        // GET: Dialysis/Create
        public IActionResult Create()
        {
            ViewData["PouchID"] = new SelectList(_context.Pouch, "PouchID", "Name");
            return View();
        }

        // POST: Dialysis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DialysisID,PouchID,DialysisDate,DialysisTime,OutWeight")] Dialysis dialysis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dialysis);
                await _context.SaveChangesAsync();

                Pouch pou = _context.Pouch.First(p => p.PouchID == dialysis.PouchID);
                pou.StockOut -= 1;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["PouchID"] = new SelectList(_context.Pouch, "PouchID", "Name", dialysis.PouchID);
            return View(dialysis);
        }

        // GET: Dialysis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dialysis = await _context.Dialysis.FindAsync(id);
            if (dialysis == null)
            {
                return NotFound();
            }
            ViewData["PouchID"] = new SelectList(_context.Pouch, "PouchID", "Name", dialysis.PouchID);
            return View(dialysis);
        }

        // POST: Dialysis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DialysisID,PouchID,DialysisDate,DialysisTime,OutWeight")] Dialysis dialysis)
        {
            if (id != dialysis.DialysisID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dialysis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DialysisExists(dialysis.DialysisID))
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
            ViewData["PouchID"] = new SelectList(_context.Pouch, "PouchID", "Name", dialysis.PouchID);
            return View(dialysis);
        }

        // GET: Dialysis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dialysis = await _context.Dialysis
                .Include(d => d.Pouch)
                .FirstOrDefaultAsync(m => m.DialysisID == id);
            if (dialysis == null)
            {
                return NotFound();
            }

            return View(dialysis);
        }

        // POST: Dialysis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dialysis = await _context.Dialysis.FindAsync(id);
            _context.Dialysis.Remove(dialysis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DialysisExists(int id)
        {
            return _context.Dialysis.Any(e => e.DialysisID == id);
        }
    }
}
