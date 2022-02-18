
using Garage2._0.Data;
using Garage2._0.Models;
using Microsoft.EntityFrameworkCore;


namespace Garage2._0.Data
{
    public class SeedData
    {

        public static async Task InitAsync(Garage2_0Context db)
        {
            if (await db.VehicleType.AnyAsync()) return;

            var vehicleTypes = GetVehicleTypes();
            await db.AddRangeAsync(vehicleTypes);

            await db.SaveChangesAsync();
        }

        private static IEnumerable<VehicleType> GetVehicleTypes()
        {
            var vehicleTypes = new List<VehicleType>();


            vehicleTypes.Add(new VehicleType("Motorcycle", 1));
            vehicleTypes.Add(new VehicleType("Tractor", 2));
            vehicleTypes.Add(new VehicleType("Bus", 3));
            vehicleTypes.Add(new VehicleType("Car", 1));
            vehicleTypes.Add(new VehicleType("Truck", 2));
            vehicleTypes.Add(new VehicleType("Forklift", 1));



            
            return vehicleTypes;
        }
    }
}