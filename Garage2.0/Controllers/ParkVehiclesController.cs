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

        public async Task<IActionResult> Filter(string? regSearch, string? colSearch, string? brandSearch, string modelSearch, int? wheelSearch, int? vehicleType)
        {
            var model = string.IsNullOrWhiteSpace(regSearch) ?
                                    _context.ParkVehicle :
                                    _context.ParkVehicle.Where(m => m.RegNumber.Contains(regSearch));
            
            model = string.IsNullOrWhiteSpace(colSearch) ?
                          model :
                          model.Where(m => m.Color.Contains(colSearch));

            model = string.IsNullOrWhiteSpace(brandSearch) ?
                          model :
                          model.Where(m => m.Color.Contains(brandSearch));

            model = string.IsNullOrWhiteSpace(modelSearch) ?
                          model :
                          model.Where(m => m.Color.Contains(modelSearch));

            model = wheelSearch == null ?
                 model :
                 model.Where(m => m.Wheels == wheelSearch);


            model = vehicleType == null ?
                             model :
                             model.Where(m => (int)m.VehicleType== vehicleType);

            return View(nameof(Index), await model.ToListAsync());
        }


        public async Task<IActionResult> Index(string searchString,string sortOrder)
        {
            ViewBag.VehicleSortParm = String.IsNullOrEmpty(sortOrder) ? "vehicle_desc" : "";
            ViewBag.RegNumberSortParm = sortOrder == "RegNumber" ? "RegNumber_desc" : "RegNumber";
            ViewBag.ColorSortParm = sortOrder == "Color" ? "Color_desc" : "Color";
            ViewBag.BrandSortParm = sortOrder == "Brand" ? "Brand_desc" : "Brand";
            ViewBag.ModelSortParm = sortOrder == "Model" ? "Model_desc" : "Model";
            ViewBag.WheelsSortParm = sortOrder == "Wheels" ? "Wheels_desc" : "Wheels";
            ViewBag.CheckInTimeSortParm = sortOrder == "Date" ? "CheckInTime_desc" : "Date";

            var vehicles = from v in _context.ParkVehicle
                           select v;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(s => s.RegNumber!.Contains(searchString));
                return View(await vehicles.ToListAsync());
            }

            switch (sortOrder)
            {
                case "vehicle_desc":
                    vehicles = vehicles.OrderByDescending(v => v.VehicleType);
                    break;
                case "RegNumber":
                    vehicles = vehicles.OrderBy(v => v.RegNumber);
                    break;
                case "RegNumber_desc":
                    vehicles = vehicles.OrderByDescending(v => v.RegNumber);
                    break;
                case "Color":
                    vehicles = vehicles.OrderBy(v => v.Color);
                    break;
                case "Color_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Color);
                    break;
                case "Brand":
                    vehicles = vehicles.OrderBy(v => v.Brand);
                    break;
                case "Brand_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Brand);
                    break;
                case "Model":
                    vehicles = vehicles.OrderBy(v => v.Model);
                    break;
                case "Model_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Model);
                    break;
                case "Wheels":
                    vehicles = vehicles.OrderBy(v => v.Wheels);
                    break;
                case "Wheels_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Wheels);
                    break;
                case "Date":
                    vehicles = vehicles.OrderBy(v => v.CheckInTime);
                    break;
                case "CheckInTime_desc":
                    vehicles = vehicles.OrderByDescending(v => v.CheckInTime);
                    break;
                   
                default:
                    vehicles = vehicles.OrderBy(v => v.VehicleType);
                    break;
            }
            return View(await vehicles.AsNoTracking().ToListAsync());
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
            var regNrDuplicate = await _context.ParkVehicle.FirstOrDefaultAsync(x => x.RegNumber == parkVehicle.RegNumber);

            var modelValid = ModelState.IsValid;

            if (modelValid)
            {
                if (regNrDuplicate == default)
                {
                    parkVehicle.CheckInTime = DateTime.Now;
                    _context.Add(parkVehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }

                ModelState.AddModelError(nameof(parkVehicle.RegNumber), "The RegNr needs to be unique!");
                ModelState.AddModelError("", "Could not be parked");
                return View();
            }
            TempData["Success"] = true;
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
                ModelState.AddModelError("", "Could not be edited");
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

            if (parkVehicle != null)
            {
                _context.ParkVehicle.Remove(parkVehicle);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpPost, ActionName("DeleteReceipt")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Receipt(int id)
        {
            var currentTime = DateTime.Now;
            var priceRate = 1;
            var parkVehicle = await _context.ParkVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkVehicle != default)
            {
                var receipt = new ReceiptViewModel
                {
                    Id = id,
                    VehicleType = parkVehicle.VehicleType,
                    RegNumber = parkVehicle.RegNumber,
                    Color = parkVehicle.Color,
                    Brand = parkVehicle.Brand,
                    Model = parkVehicle.Model,
                    Wheels = parkVehicle.Wheels,
                    CheckInTime = parkVehicle.CheckInTime,
                    CheckOutTime = currentTime,
                    ParkedTime = currentTime - parkVehicle.CheckInTime,
                    Price = 5 + (int)(currentTime - parkVehicle.CheckInTime).TotalMinutes * priceRate

                };

                _context.ParkVehicle.Remove(parkVehicle);
                await _context.SaveChangesAsync();
                return View(receipt);

            }
            return RedirectToAction(nameof(Index));
        }

        private bool ParkVehicleExists(int id)
        {
            return _context.ParkVehicle.Any(e => e.Id == id);
        }
    }
}
