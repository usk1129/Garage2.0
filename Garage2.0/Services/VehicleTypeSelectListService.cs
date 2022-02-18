using Garage2._0.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0.Services
{
    public class VehicleTypeSelectListService : IVehicleTypeSelectListService
    {
        private readonly Garage2_0Context db;
        public VehicleTypeSelectListService(Garage2_0Context db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<SelectListItem>> GetActiveVehicleTypesAsync()
        {
            return await db.ParkVehicle
            .Select(v => v.VehicleType)
            .Distinct()
            .Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Id.ToString()
            })
            .ToListAsync();
        }
        public async Task<IEnumerable<SelectListItem>> GetAllVehicleTypesAsync()
        {
            return await db.VehicleType
            .Select(v => v)
            .Distinct()
            .Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Id.ToString()
            })
            .ToListAsync();
        }
    }
}
