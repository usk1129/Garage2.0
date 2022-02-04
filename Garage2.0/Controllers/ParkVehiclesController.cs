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
using Garage2._0.Helpers;

namespace Garage2._0.Controllers
{
    public class ParkVehiclesController : Controller
    {
        private readonly Garage2_0Context _context;
        private readonly IConfiguration _configuration;
        public ParkVehiclesController(Garage2_0Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
                          model.Where(m => m.Brand.Contains(brandSearch));

            model = string.IsNullOrWhiteSpace(modelSearch) ?
                          model :
                          model.Where(m => m.Model.Contains(modelSearch));

            model = wheelSearch == null ?
                 model :
                 model.Where(m => m.Wheels == wheelSearch);


            model = vehicleType == null ?
                             model :
                             model.Where(m => (int)m.VehicleType == vehicleType);

            return View(nameof(Index), await model.ToListAsync());
        }

        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewBag.VehicleSortParm = String.IsNullOrEmpty(sortOrder) ? "vehicle_desc" : "";
            ViewBag.ParkingSortParm = String.IsNullOrEmpty(sortOrder) ? "park_desc" : "park";
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
                case "park_desc":
                    vehicles = vehicles.OrderByDescending(v => v.ParkingSlot);
                    break;
                case "park":
                    vehicles = vehicles.OrderBy(v => v.ParkingSlot);
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
        public async Task<IActionResult> Create([Bind("Id,VehicleType,ParkingSlot,RegNumber,Color,Brand,Model,Wheels,CheckInTime")] ParkVehicle parkVehicle)
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
                    TempData["Success"] = $"{parkVehicle.RegNumber} is successfully parked";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.Clear();
                ModelState.AddModelError(nameof(parkVehicle.RegNumber), "The RegNr needs to be unique!");
                ModelState.AddModelError("", "Could not park, something went wrong!");
                return View();
            }
            

            return View(parkVehicle);
        }

        [HttpGet]
        public ActionResult GetParkingSlots(int vehicleType)
        {
            IEnumerable<SelectListItem> slots = GetSlots(vehicleType);
            return Json(slots);
        }

        private Dictionary<string, string> GetExistingParkedVechiles()
        {

            Dictionary<string, string> slotMap = new Dictionary<string, string>();

            var vehicles = from v in _context.ParkVehicle
                           select v;
            vehicles = vehicles.OrderByDescending(v => v.ParkingSlot);

            
            slotMap = vehicles.ToDictionary(v => v.ParkingSlot.ToString(), v => v.VehicleType.ToString());

            return slotMap;

        }

        private IEnumerable<SelectListItem> GetSlots(int vehicleType)
        {
            List<SelectListItem> parkingSlots = new List<SelectListItem>();

            Dictionary<string, string> slotMap = GetExistingParkedVechiles();

            parkingSlots = GetFreeSlots(vehicleType, slotMap);

            return parkingSlots;

        }

        private bool slotOccupied(string i, Dictionary<string, string> slotMap)
        {

            foreach (var item in slotMap)
            {
                string[] slots = item.Key.Split(',');
                for (int j = 0; j < slots.Length; j++)
                {
                    if (slots[j].Equals(i))
                        return true;
                }
            }
            return false;
        }

        public async Task<IActionResult> GarageSlots()
        {
            int garageCapacity = 0;
            var model = _context.ParkVehicle;
            

            Dictionary<int, string> currentSlots = new Dictionary<int, string>();
            Dictionary<string, string> slotMap = GetExistingParkedVechiles();

            if (int.TryParse(_configuration.GetSection("GarageCapacity").Value, out garageCapacity) is true)
            {
                if (garageCapacity > 0)
                {
                    for (int i = 1; i <= garageCapacity; i++)
                    {
                        if (slotOccupied(i.ToString(), slotMap))
                        {
                            currentSlots.Add(i, "Occupied");
                        }
                        else
                        {
                            currentSlots.Add(i, "Empty");
                        }
                    }
                }
            }

            List<GarageSlotModel> slotCollection = new List<GarageSlotModel>();

            foreach (var item in currentSlots)
            {
                slotCollection.Add(new GarageSlotModel { Capacity = garageCapacity, Slot = item.Key, Occupancy = item.Value });
            }

            return View(slotCollection.ToList());
            //return View(nameof(Index), await model.ToListAsync());

        }


        private List<SelectListItem> GetFreeSlots(int vehicleType, Dictionary<string, string> slotMap)
        {

            List<SelectListItem> parkingSlots = new List<SelectListItem>();
            int garageCapacity = -1;
            Dictionary<int, string> currentSlots = new Dictionary<int, string>();

            if (int.TryParse(_configuration.GetSection("GarageCapacity").Value, out garageCapacity) is true)
            {
                if (garageCapacity > 0)
                {
                    for (int i = 1; i <= garageCapacity; i++)
                    {
                        if (slotOccupied(i.ToString(), slotMap)) 
                        {
                            currentSlots.Add(i, "Occupied");
                        }
                        else
                        {
                            currentSlots.Add(i, "Empty");
                        }
                    }
                }
            }

           // GarageSlots gSlots = new GarageSlots(garageCapacity, currentSlots);
            
            switch (vehicleType)
            {
                case (int)VehicleType.Bus:
                    parkingSlots = GetNextAvailableSlot(3, currentSlots, garageCapacity, vehicleType);
                    break;
                case (int)VehicleType.Car:
                    parkingSlots = GetNextAvailableSlot(1, currentSlots, garageCapacity, vehicleType);
                    break;
                case (int)VehicleType.Forklift:
                    parkingSlots = GetNextAvailableSlot(1, currentSlots, garageCapacity, vehicleType);
                    break;
                case (int)VehicleType.Motorcycle:
                    parkingSlots = GetNextAvailableSlot(1, currentSlots, garageCapacity, vehicleType);
                    break;
                case (int)VehicleType.Truck:
                    parkingSlots = GetNextAvailableSlot(2, currentSlots, garageCapacity, vehicleType);
                    break;
                case (int)VehicleType.Tractor:
                    parkingSlots = GetNextAvailableSlot(2, currentSlots, garageCapacity, vehicleType);
                    break;

            }
            return parkingSlots;

        }


        private List<SelectListItem> GetNextAvailableSlot(int consecutiveSlotsNeeded, Dictionary<int, string> currentSlots, int garageCapacity, int vehicleType)
        {
            string nextSlot = "";
            bool slotFound = false;

            List<SelectListItem> parkingSlots = new List<SelectListItem>();
            parkingSlots.Clear();

            for (int i = 0; i < currentSlots.Count; i++)
            {
                slotFound = false;
                nextSlot = "";
                //already peeking at the end with more space needed. so not found
                if (i + consecutiveSlotsNeeded > garageCapacity)
                {
                    slotFound = false;
                    break;
                }
                for (int j = 0; j < consecutiveSlotsNeeded; j++)
                {
                    if (currentSlots.ElementAt(i + j).Value.Equals("Empty"))
                    {
                        slotFound = true;
                        nextSlot += "," + currentSlots.ElementAt(i + j).Key;
                    }
                    else
                    {
                        slotFound = false;
                        break;
                    }
                }
                if (slotFound)
                {
                    //found it
                    break;
                }
            }

            nextSlot = nextSlot.TrimStart(',');
            if (slotFound is false)
            {
                parkingSlots.Add
                    (
                        new SelectListItem
                        {
                            Value = "-1",
                            Text = "Not Possible to park " + Enum.GetName(typeof(VehicleType), vehicleType)
                        }
                    );
            }
            else
            {
                parkingSlots.Add
                    (
                        new SelectListItem
                        {
                            Value =  nextSlot,
                            Text = nextSlot
                        }
                    );
            }
            return parkingSlots;
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleType,ParkingSlot, RegNumber,Color,Brand,Model,Wheels,CheckInTime")] ParkVehicle parkVehicle)
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

                    TempData["Success"] = $"{parkVehicle.RegNumber} is successfully edited";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(nameof(parkVehicle.RegNumber), "The RegNr needs to be unique!");
                ModelState.AddModelError("", "Could not edit, something went wrong!");
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
                TempData["Success"] = $"{parkVehicle.RegNumber} has been checked out!";
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpPost, ActionName("DeleteReceipt")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Receipt(int id)
        {
            var currentTime = DateTime.Now;
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
                    Price = CalcPrice(parkVehicle.CheckInTime, currentTime)

                };

                _context.ParkVehicle.Remove(parkVehicle);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"{parkVehicle.RegNumber} has successfully been checked out!";
                return View(receipt);

            }
            return RedirectToAction(nameof(Index));
        }

        private int CalcPrice(DateTime checkInTime, DateTime currentTime)
        { 
        int priceRate = 1;           
        return 5 + (int)(currentTime - checkInTime).TotalMinutes * priceRate;
        }

        public async Task<IActionResult> Statistics()
        {

            int wheels = 0;
            int totalVehicles = 0;
            int currentFees = 0;
            var currentTime = DateTime.Now;

           List<VehicleTypeHelper> vehicleTypeAmounts = await _context.ParkVehicle.GroupBy(t => t.VehicleType)
                                       .Select(t => new VehicleTypeHelper
                                       {
                                           Category = t.Key,
                                           Count = t.Count()                                           
                                       }).ToListAsync();



            await _context.ParkVehicle.ForEachAsync(x => { 
                                        wheels += x.Wheels; 
                                        totalVehicles += 1;
                                        currentFees += CalcPrice(x.CheckInTime, currentTime);
                                        
             });

            var viewModel = new StatisticsViewModel
            {
                VehicleTypeAmount = vehicleTypeAmounts,
                AmountOfWheels = wheels,
                AmountOfVehicles = totalVehicles,
                CurrentFees = currentFees

            };
            
   

            return View(viewModel);
        }

        private bool ParkVehicleExists(int id)
        {
            return _context.ParkVehicle.Any(e => e.Id == id);
        }
    }
}
