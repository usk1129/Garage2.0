using Garage2._0.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0.Services
{
    public class ParkingSpotSelectListService : IParkingSpotSelectListService
    {
        private readonly Garage2_0Context db;
        public ParkingSpotSelectListService(Garage2_0Context db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<SelectListItem>> GetEmptyParkingSpotsAsync()
        {

            return await db.ParkingSpot.Where(v => v.ParkVehicleID == null)
           .Select(v => v)
           .Select(t => new SelectListItem
           {
               Text = t.ParkingSpotNr.ToString(),
               Value = t.ParkingSpotNr.ToString()
           }).ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetParkingSpotsAsync()
        {

            return await db.ParkingSpot
           .Select(v => v)
           .Select(t => new SelectListItem
           {
               Text = t.ParkingSpotNr.ToString(),
               Value = t.ParkingSpotNr.ToString()
           }).ToListAsync();


        }
    }
}
