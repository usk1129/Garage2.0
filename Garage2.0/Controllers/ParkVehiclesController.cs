#nullable disable
using Garage2._0.Data;
using Garage2._0.Helpers;
using Garage2._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Filter(string? regSearch, string? colSearch, string? brandSearch, string modelSearch, int? wheelSearch, string? vehicleType)
        {
            var model = string.IsNullOrWhiteSpace(regSearch) ?
                                    _context.ParkVehicle :
                                    _context.ParkVehicle.Include(m => m.VehicleType).Where(m => m.RegNumber.Contains(regSearch));

            model = string.IsNullOrWhiteSpace(colSearch) ?
                          model :
                          model.Include(m => m.VehicleType).Where(m => m.Color.Contains(colSearch));

            model = string.IsNullOrWhiteSpace(brandSearch) ?
                          model :
                          model.Include(m => m.VehicleType).Where(m => m.Brand.Contains(brandSearch));

            model = string.IsNullOrWhiteSpace(modelSearch) ?
                          model :
                          model.Include(m => m.VehicleType).Where(m => m.Model.Contains(modelSearch));

            model = wheelSearch == null ?
                 model :
                 model.Include(m => m.VehicleType).Where(m => m.Wheels == wheelSearch);


            model = vehicleType == null ?
                             model :
                             model.Include(m => m.VehicleType).Where(m => m.VehicleType.Name == vehicleType);

            return View(nameof(Index), await model.ToListAsync());
        }
        public async Task<IActionResult> Index2(string? sortOrder)
        {

            IQueryable<ParkVehicle> vehicles = _context.ParkVehicle.Where(v => v.ParkingSpotId != null).Include(v => v.VehicleType).Include(v => v.Parkings).Include(v => v.Member);
            List<IndexViewModel> model = new List<IndexViewModel>();
            var timeNow = DateTime.Now;
            foreach (var vehicle in vehicles)
            {

                model.Add(new IndexViewModel
                {
                    RegNumber = vehicle.RegNumber,
                    Color = vehicle.Color,
                    Brand = vehicle.Brand,
                    Model = vehicle.Model,
                    Wheels = vehicle.Wheels,
                    ParkingSpotNR = (int)vehicle.ParkingSpotId,
                    Owner = vehicle.Member.GetFullName(),
                    VehicleType = vehicle.VehicleType.Name,
                    ParkTime = (TimeSpan)(timeNow - vehicle.CheckInTime)


                });

            }
            ViewBag.MemberSortParm = sortOrder == "Member" ? "member_desc" : "Member";
            ViewBag.VehicleSortParm = String.IsNullOrEmpty(sortOrder) ? "vehicle_desc" : "";
            ViewBag.ParkingSortParm = sortOrder == "Parking Slot" ? "park_desc" : "Parking Slot";
            ViewBag.RegNumberSortParm = sortOrder == "RegNumber" ? "RegNumber_desc" : "RegNumber";
            ViewBag.ColorSortParm = sortOrder == "Color" ? "Color_desc" : "Color";
            ViewBag.BrandSortParm = sortOrder == "Brand" ? "Brand_desc" : "Brand";
            ViewBag.ModelSortParm = sortOrder == "Model" ? "Model_desc" : "Model";
            ViewBag.WheelsSortParm = sortOrder == "Wheels" ? "Wheels_desc" : "Wheels";
            ViewBag.DurationSortParm = sortOrder == "Duration" ? "Duration_desc" : "Duration";

            switch (sortOrder)
            {

                case "vehicle_desc":
                    model = model.OrderByDescending(v => v.VehicleType).ToList();
                    break;
                case "park_desc":
                    model = model.OrderByDescending(v => v.ParkingSpotNR).ToList();
                    break;
                case "Parking Slot":
                    model = model.OrderBy(v => v.ParkingSpotNR).ToList();
                    break;
                case "RegNumber":
                    model = model.OrderBy(v => v.RegNumber).ToList();
                    break;
                case "RegNumber_desc":
                    model = model.OrderByDescending(v => v.RegNumber).ToList();
                    break;
                case "Color":
                    model = model.OrderBy(v => v.Color).ToList();
                    break;
                case "Color_desc":
                    model = model.OrderByDescending(v => v.Color).ToList();
                    break;
                case "Brand":
                    model = model.OrderBy(v => v.Brand).ToList();
                    break;
                case "Brand_desc":
                    model = model.OrderByDescending(v => v.Brand).ToList();
                    break;
                case "Model":
                    model = model.OrderBy(v => v.Model).ToList();
                    break;
                case "Model_desc":
                    model = model.OrderByDescending(v => v.Model).ToList();
                    break;
                case "Wheels":
                    model = model.OrderBy(v => v.Wheels).ToList();
                    break;
                case "Wheels_desc":
                    model = model.OrderByDescending(v => v.Wheels).ToList();
                    break;
                case "Duration":
                    model = model.OrderBy(v => v.ParkTime).ToList();
                    break;
                case "Duration_desc":
                    model = model.OrderByDescending(v => v.ParkTime).ToList();
                    break;

                case "member_desc":
                    model = model.OrderByDescending(v => v.Owner).ToList();
                    break;

                case "Member":
                    model = model.OrderBy(v => v.Owner).ToList();
                    break;

                default:
                    model = model.OrderBy(v => v.VehicleType).ToList();
                    break;
            }

            return View(nameof(Index2), model);
        }
        public async Task<IActionResult> GarageSlots()
        {
            var spots = await _context.ParkingSpot.ToListAsync();

            List<GarageSlotModel> parkingSpots = new List<GarageSlotModel>(); ;

            foreach (var item in spots)
            {
                string occupancy = "";
                if (item.ParkVehicleID != null)
                    occupancy = "Taken";
                else
                    occupancy = "Open";
                parkingSpots.Add(new GarageSlotModel
                {
                    Slot = item.ParkingSpotNr,
                    Occupancy = occupancy


                });
            }

            return View(parkingSpots);

        }

        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewBag.VehicleSortParm = String.IsNullOrEmpty(sortOrder) ? "vehicle_desc" : "";
            ViewBag.ParkingSortParm = sortOrder == "Parking Slot" ? "park_desc" : "Parking Slot";
            ViewBag.RegNumberSortParm = sortOrder == "RegNumber" ? "RegNumber_desc" : "RegNumber";
            ViewBag.ColorSortParm = sortOrder == "Color" ? "Color_desc" : "Color";
            ViewBag.BrandSortParm = sortOrder == "Brand" ? "Brand_desc" : "Brand";
            ViewBag.ModelSortParm = sortOrder == "Model" ? "Model_desc" : "Model";
            ViewBag.WheelsSortParm = sortOrder == "Wheels" ? "Wheels_desc" : "Wheels";
            ViewBag.CheckInTimeSortParm = sortOrder == "Date" ? "CheckInTime_desc" : "Date";

            IQueryable<ParkVehicle> vehicles = _context.ParkVehicle.Include(v => v.VehicleType).Include(v => v.Parkings);

            //var vehicles = from v in _context.ParkVehicle                           
            //               select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(s => s.RegNumber!.Contains(searchString));
                return View(await vehicles.ToListAsync());
            }

            switch (sortOrder)
            {
                case "vehicle_desc":
                    vehicles = vehicles.OrderByDescending(v => v.VehicleType.Name);
                    break;
                //case "park_desc":
                //    vehicles = vehicles.OrderByDescending(v => v.ParkingSlot);
                //    break;
                //case "Parking Slot":
                //    vehicles = vehicles.OrderBy(v => v.ParkingSlot);
                //    break;
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
                    vehicles = vehicles.OrderBy(v => v.VehicleType.Name);
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
                .Include(v => v.Member)
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
        public IActionResult CheckInMember()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckInMember(CheckInMemberVehicleViewModel parkVehicle)
        {

            //parkVehicle.Member = member;

            var modelValid = ModelState.IsValid;

            if (modelValid)
            {

                var member = await _context.Member.FindAsync(parkVehicle.MemberId);
                // var vehicle = await _context.ParkVehicle.FindAsync(parkVehicle.VehicleId);

                var vehicle = await _context.ParkVehicle
                    .Include(v => v.VehicleType)
                    .FirstOrDefaultAsync(v => v.Id == parkVehicle.VehicleId);
                //// parkVehicle.CheckInTime = DateTime.Now;
                //ParkingSpot spot = await _context.ParkingSpot.FirstOrDefaultAsync(t => t.ParkVehicle == null);
                // //spot.ParkVehicle = vehicle;
                // //vehicle.ParkingSpotId = spot.Id;
                // _context.Update(vehicle);
                // _context.Update(spot);
                // await _context.SaveChangesAsync();


                // TempData["Success"] = $"{vehicle.RegNumber} is successfully parked";

                var parkingSpots = await GetAvailableParkingSpotsAsync(vehicle.VehicleType.Size);


                var model = new PickParkingSpotViewModel
                {

                    MemberName = member.GetFullName(),
                    RegNR = vehicle.RegNumber,
                    VehicleId = vehicle.Id,
                    Size = vehicle.VehicleType.Size,
                    ParkingSpots = parkingSpots.Select(v => v).Select(t => new SelectListItem
                    {
                        Text = t.ParkingSpotNr.ToString(),
                        Value = t.ParkingSpotNr.ToString()
                    }).ToList()


                };

                return View(nameof(PickParkingSpot), model);

            }


            return View(parkVehicle);
        }

        private async Task<List<ParkingSpot>> GetAvailableParkingSpotsAsync(int size)
        {
            var emptySpots = await _context.ParkingSpot.Select(v => v).ToListAsync();
            List<ParkingSpot> freeSpots = new List<ParkingSpot>();
            Queue<ParkingSpot> parkingSpotsSequence = new Queue<ParkingSpot>();
            List<ParkingSpot> parkingSpotAvailable = new List<ParkingSpot>();
            foreach (var spot in emptySpots)
            {
                if (spot.ParkVehicleID == null)
                {
                    parkingSpotsSequence.Enqueue(spot);
                    if (parkingSpotsSequence.Count == size)
                    {
                        parkingSpotAvailable.Add(parkingSpotsSequence.Dequeue());
                    }
                }
                else
                    parkingSpotsSequence.Clear();
            }
            return parkingSpotAvailable.ToList();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PickParkingSpot(PickParkingSpotViewModel parkVehicle)
        {


            //parkVehicle.Member = member;

            var modelValid = ModelState.IsValid;

            if (modelValid)
            {

                //  var member = await _context.Member.FindAsync(parkVehicle.MemberId);
                var vehicle = await _context.ParkVehicle
                    .Include(v => v.VehicleType)
                    .FirstOrDefaultAsync(v => v.Id == parkVehicle.VehicleId);

                vehicle.CheckInTime = DateTime.Now;
                // ParkingSpot spot = await _context.ParkingSpot.FirstOrDefaultAsync(t => t.ParkingSpotNr == parkVehicle.ParkingSpotId);
                //  spot.ParkVehicle = vehicle;


                vehicle.ParkingSpotId = parkVehicle.ParkingSpotId;
                _context.Update(vehicle);
                for (int i = 0; i < vehicle.VehicleType.Size; i++)
                {
                    var spot = await _context.ParkingSpot.FirstOrDefaultAsync(t => t.ParkingSpotNr == (parkVehicle.ParkingSpotId + i));
                    vehicle.Parkings.Add(spot);
                    spot.ParkVehicle = vehicle;
                    _context.Update(spot);
                }
                _context.Update(vehicle);


                await _context.SaveChangesAsync();


                TempData["Success"] = $"{vehicle.RegNumber} is successfully parked";
                return RedirectToAction(nameof(Index));

            }


            return View(parkVehicle);

        }
        // POST: ParkVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParkVehicle parkVehicle)
        {
            var regNrDuplicate = await _context.ParkVehicle.FirstOrDefaultAsync(x => x.RegNumber == parkVehicle.RegNumber);


            var member = await _context.Member.FindAsync(parkVehicle.MemberId);
            //parkVehicle.Member = member;

            var modelValid = ModelState.IsValid;

            if (modelValid)
            {

                if (regNrDuplicate == default)
                {

                    //parkVehicle.CheckInTime = DateTime.Now;
                    VehicleType type = await _context.VehicleType.FirstOrDefaultAsync(t => t.Id == parkVehicle.VehicleTypeID);
                    parkVehicle.VehicleTypeID = type.Id;
                    type.Vehicles.Add(parkVehicle);
                    //ParkingSpot spot = await _context.ParkingSpot.FirstOrDefaultAsync(s => s.ParkingSpotNr == parkVehicle.ParkingSpotId);
                    //spot.ParkVehicle = parkVehicle;
                    await _context.SaveChangesAsync();

                    //_context.Add(parkVehicle);



                    TempData["Success"] = $"{parkVehicle.RegNumber} is successfully registered";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.Clear();
                ModelState.AddModelError(nameof(parkVehicle.RegNumber), "The RegNr needs to be unique!");
                ModelState.AddModelError("", "Could not park, something went wrong!");
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
            ParkVehicle parkVehicle = await _context.ParkVehicle.Include(v => v.VehicleType).Include(v => v.Parkings).Include(v => v.Member).FirstAsync(v => v.VehicleTypeID == id);

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
        public async Task<IActionResult> Edit(int id, ParkVehicle parkVehicle)
        {

            if (id != parkVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!await _context.ParkVehicle.AnyAsync(x => x.RegNumber == parkVehicle.RegNumber && x.Id != parkVehicle.Id))
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
                var parking = await _context.ParkingSpot.Where(x => x.ParkVehicleID == parkVehicle.Id).ToListAsync();
                foreach (var item in parking)
                {
                    item.ParkVehicleID = null;

                    _context.Update(item);
                }
                //_context.ParkVehicle.Remove(parkVehicle);
                parkVehicle.CheckInTime = null;
                parkVehicle.ParkingSpotId = null;
                await _context.SaveChangesAsync();
                TempData["Success"] = $"{parkVehicle.RegNumber} has been checked out!";
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpPost, ActionName("DeleteReceipt")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Receipt(int id)
        {

            var parkVehicle = await _context.ParkVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkVehicle.CheckInTime != null)
            {

                var currentTime = DateTime.Now;

                var member = await _context.Member.FirstAsync(m => m.Id == parkVehicle.MemberId);
                var type = await _context.VehicleType.FirstAsync(m => m.Id == parkVehicle.VehicleTypeID);
                if (parkVehicle != default)
                {
                    var receipt = new ReceiptViewModel
                    {
                        Owner = member.GetFullName(),
                        VehicleType = type.Name,
                        Id = id,
                        RegNumber = parkVehicle.RegNumber,
                        Color = parkVehicle.Color,
                        Brand = parkVehicle.Brand,
                        Model = parkVehicle.Model,
                        Wheels = parkVehicle.Wheels,
                        CheckInTime = parkVehicle.CheckInTime,
                        CheckOutTime = currentTime,
                        ParkedTime = currentTime - parkVehicle.CheckInTime,
                        Price = CalcPrice((DateTime)parkVehicle.CheckInTime, currentTime)
                        

                    };
                    var parking = await _context.ParkingSpot.Where(x => x.ParkVehicleID == parkVehicle.Id).ToListAsync();
                    foreach (var item in parking)
                    {
                        item.ParkVehicleID = null;
                        _context.Update(item);
                    }
                    // _context.ParkVehicle.Remove(parkVehicle);
                    parkVehicle.CheckInTime = null;
                    parkVehicle.ParkingSpotId = null;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"{parkVehicle.RegNumber} has successfully been checked out!";
                    return View(receipt);

                }
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

            var addedTime = DateTime.Now.AddHours(1);
            var parkedVehicles = 0;
            int predictedFees = 0;
            int members = 0;

            List<VehicleTypeHelper> vehicleTypeAmounts = await _context.ParkVehicle.GroupBy(t => t.VehicleType.Name)
                                        .Select(t => new VehicleTypeHelper
                                        {
                                            Category = t.Key,
                                            Count = t.Count()
                                        }).ToListAsync();
            members = _context.Member.Count();
            



            await _context.ParkVehicle.ForEachAsync(x =>
            {
                wheels += x.Wheels;
                totalVehicles += 1;
                if (x.CheckInTime != null)
                {
                    currentFees += CalcPrice((DateTime)x.CheckInTime, currentTime);
                    predictedFees += CalcPrice((DateTime)x.CheckInTime, addedTime);
                }
                if (x.ParkingSpotId != null)
                    parkedVehicles++;
            });

            var viewModel = new StatisticsViewModel
            {
                VehicleTypeAmount = vehicleTypeAmounts,
                AmountOfWheels = wheels,
                AmountOfVehicles = totalVehicles,
                CurrentFees = currentFees,
                AmountOfParkedVehicles = parkedVehicles,
                PredictedFees = predictedFees,
                Members = members

            };



            return View(viewModel);
        }

        private bool ParkVehicleExists(int id)
        {
            return _context.ParkVehicle.Any(e => e.Id == id);
        }

        public IActionResult AddVehicleType()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicleType([Bind("Name, Size")] VehicleType vehicleType)
        {

            var modelValid = ModelState.IsValid;

            if (modelValid)
            {

                _context.Add(vehicleType);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"{vehicleType.Name} is successfully added";
                return RedirectToAction(nameof(Index));

            }


            return View(vehicleType);
        }
        public async Task<JsonResult> GetSubCategoryByMemberIdAsync(int memberId)
        {;
            var data = await _context.ParkVehicle.Where(v => v.MemberId == memberId && v.ParkingSpotId == null)
             .Select(v => v)
             .Select(t => new CheckInMemberVehicleViewModel
             {
                 MemberId = memberId,
                VehicleId = t.Id,
                VehicleName = t.RegNumber
             })
             .ToListAsync();

            return Json(data);
        }
    }
}
