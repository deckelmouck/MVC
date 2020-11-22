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
    public class DeliveryPouchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryPouchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryPouches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DeliveryPouch.Include(d => d.Delivery).Include(d => d.Pouch);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DeliveryPouches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPouch = await _context.DeliveryPouch
                .Include(d => d.Delivery)
                .Include(d => d.Pouch)
                .FirstOrDefaultAsync(m => m.DeliveryPouchID == id);
            if (deliveryPouch == null)
            {
                return NotFound();
            }

            return View(deliveryPouch);
        }

        // GET: DeliveryPouches/Create
        public IActionResult Create()
        {
            ViewData["DeliveryID"] = new SelectList(_context.Delivery, "DeliveryID", "DeliveryDate");
            ViewData["PouchID"] = new SelectList(_context.Pouch, "PouchID", "Name");
            return View();
        }

        // POST: DeliveryPouches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryPouchID,DeliveryID,PouchID,OrderQuantity,Delivered")] DeliveryPouch deliveryPouch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryPouch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeliveryID"] = new SelectList(_context.Delivery, "DeliveryID", "DeliveryID", deliveryPouch.DeliveryID);
            ViewData["PouchID"] = new SelectList(_context.Pouch, "PouchID", "PouchID", deliveryPouch.PouchID);
            return View(deliveryPouch);
        }

        // GET: DeliveryPouches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPouch = await _context.DeliveryPouch.FindAsync(id);
            if (deliveryPouch == null)
            {
                return NotFound();
            }
            ViewData["DeliveryID"] = new SelectList(_context.Delivery, "DeliveryID", "DeliveryID", deliveryPouch.DeliveryID);
            ViewData["PouchID"] = new SelectList(_context.Pouch, "PouchID", "PouchID", deliveryPouch.PouchID);
            return View(deliveryPouch);
        }

        // POST: DeliveryPouches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryPouchID,DeliveryID,PouchID,OrderQuantity,Delivered")] DeliveryPouch deliveryPouch)
        {
            if (id != deliveryPouch.DeliveryPouchID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryPouch);
                    await _context.SaveChangesAsync();

					//TODO issue #6 - https://github.com/deckelmouck/MVC/issues/6
					if (deliveryPouch.Delivered)
                    {
                        Pouch pouch = _context.Pouch.First(p => p.PouchID == deliveryPouch.PouchID);
                        pouch.StockOut += deliveryPouch.OrderQuantity;
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryPouchExists(deliveryPouch.DeliveryPouchID))
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
            ViewData["DeliveryID"] = new SelectList(_context.Delivery, "DeliveryID", "DeliveryID", deliveryPouch.DeliveryID);
            ViewData["PouchID"] = new SelectList(_context.Pouch, "PouchID", "PouchID", deliveryPouch.PouchID);
            return View(deliveryPouch);
        }

        // GET: DeliveryPouches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPouch = await _context.DeliveryPouch
                .Include(d => d.Delivery)
                .Include(d => d.Pouch)
                .FirstOrDefaultAsync(m => m.DeliveryPouchID == id);
            if (deliveryPouch == null)
            {
                return NotFound();
            }

            return View(deliveryPouch);
        }

        // POST: DeliveryPouches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryPouch = await _context.DeliveryPouch.FindAsync(id);
            _context.DeliveryPouch.Remove(deliveryPouch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryPouchExists(int id)
        {
            return _context.DeliveryPouch.Any(e => e.DeliveryPouchID == id);
        }
    }
}
