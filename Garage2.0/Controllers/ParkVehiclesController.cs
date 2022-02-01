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

        public async Task<IActionResult> Filter(string regSearch, int? vehicleType)
        {
            var model = string.IsNullOrWhiteSpace(regSearch) ?
                                    _context.ParkVehicle :
                                    _context.ParkVehicle.Where(m => m.RegNumber.Contains(regSearch));
               model = vehicleType == null ?
                             model :
                             model.Where(m => (int)m.VehicleType== vehicleType);

            return View(nameof(Index), await model.ToListAsync());
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var vehicles = from v in _context.ParkVehicle
                           select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(s => s.RegNumber!.Contains(searchString));
                return View(await vehicles.ToListAsync());
            }
            else
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
                
                if (!_context.ParkVehicle.Any(x => x.RegNumber == parkVehicle.RegNumber))
                { 
                _context.Add(parkVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
                }

                ModelState.AddModelError(nameof(parkVehicle.RegNumber), "The RegNr needs to be unique!");
                return View();
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
                if (!_context.ParkVehicle.Any(x => x.RegNumber == parkVehicle.RegNumber && x.Id != parkVehicle.Id))
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
                ModelState.AddModelError(nameof(parkVehicle.RegNumber), "The RegNr needs to be unique!");
                return View(); 
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
