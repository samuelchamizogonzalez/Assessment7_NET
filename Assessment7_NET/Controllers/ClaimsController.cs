using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assessment7_NET.Models;

namespace Assessment7_NET.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly SamuelchamizodatabaseContext _context;

        public ClaimsController(SamuelchamizodatabaseContext context)
        {
            _context = context;
        }

        // GET: Claims
        public async Task<IActionResult> Index(String vin)
        {
            var claims = from Claim in _context.Claims select Claim;

            if (!String.IsNullOrEmpty(vin))
            {
                claims = claims.Where(s => s.IdVehicleNavigation.Vin.Contains(vin)).Include(v => (v.IdVehicleNavigation));
                return View(claims);
            }

            return View(await claims.ToListAsync());
        }

        // GET: Claims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Claims == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims
                .Include(c => c.IdVehicleNavigation)
                .FirstOrDefaultAsync(m => m.IdClaim == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            ViewData["IdVehicle"] = new SelectList(_context.Vehicles, "IdVehicle", "IdVehicle");
            return View();
        }

        // POST: Claims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClaim,Description,Status,Date,IdVehicle")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdVehicle"] = new SelectList(_context.Vehicles, "IdVehicle", "IdVehicle", claim.IdVehicle);
            return View(claim);
        }

        // GET: Claims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Claims == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            ViewData["IdVehicle"] = new SelectList(_context.Vehicles, "IdVehicle", "IdVehicle", claim.IdVehicle);
            return View(claim);
        }

        // POST: Claims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClaim,Description,Status,Date,IdVehicle")] Claim claim)
        {
            if (id != claim.IdClaim)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimExists(claim.IdClaim))
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
            ViewData["IdVehicle"] = new SelectList(_context.Vehicles, "IdVehicle", "IdVehicle", claim.IdVehicle);
            return View(claim);
        }

        // GET: Claims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Claims == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims
                .Include(c => c.IdVehicleNavigation)
                .FirstOrDefaultAsync(m => m.IdClaim == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Claims == null)
            {
                return Problem("Entity set 'SamuelchamizodatabaseContext.Claims'  is null.");
            }
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                _context.Claims.Remove(claim);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimExists(int id)
        {
          return _context.Claims.Any(e => e.IdClaim == id);
        }
    }
}
