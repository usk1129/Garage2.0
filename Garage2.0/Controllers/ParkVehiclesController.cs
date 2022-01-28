#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2._0.Data;
using Garage2._0.Models;

namespace Garage2._0.Controllers
{
    public class ParkVehiclesController : Controller
    {
        private readonly Garage2_0Context _context;

        public ParkVehiclesController(Garage2_0Context context)
        {
            _context = context;
        }

        // GET: ParkVehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParkVehicle.ToListAsync());
        }

        // GET: ParkVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkVehicle = await _context.ParkVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkVehicle == null)
            {
                return NotFound();
            }

            return View(parkVehicle);
        }

        // GET: ParkVehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParkVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleType,RegNumber,Color,Brand,Model,Wheels,CheckInTime")] ParkVehicle parkVehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkVehicle);
        }

        // GET: ParkVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkVehicle = await _context.ParkVehicle.FindAsync(id);
            if (parkVehicle == null)
            {
                return NotFound();
            }
            return View(parkVehicle);
        }

        // POST: ParkVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleType,RegNumber,Color,Brand,Model,Wheels,CheckInTime")] ParkVehicle parkVehicle)
        {
            if (id != parkVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkVehicleExists(parkVehicle.Id))
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
            return View(parkVehicle);
        }

        // GET: ParkVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkVehicle = await _context.ParkVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkVehicle == null)
            {
                return NotFound();
            }

            return View(parkVehicle);
        }

        // POST: ParkVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkVehicle = await _context.ParkVehicle.FindAsync(id);
            _context.ParkVehicle.Remove(parkVehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkVehicleExists(int id)
        {
            return _context.ParkVehicle.Any(e => e.Id == id);
        }
    }
}
