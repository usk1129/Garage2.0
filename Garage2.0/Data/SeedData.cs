
using Garage2._0.Data;
using Garage2._0.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Garage2._0.Data
{
    public class SeedData
    {

        public static async Task InitAsync(Garage2_0Context db, IConfiguration configuration)
        {
            if (await db.VehicleType.AnyAsync()) return;

            var vehicleTypes = GetVehicleTypes();
            var members = GetMembers();
            var parkSpots = GetParkinSpots(configuration);
            //var vehicles = GetVehicles();

        

            await db.AddRangeAsync(members);
            await db.AddRangeAsync(vehicleTypes);
            await db.AddRangeAsync(parkSpots);

            

            await db.SaveChangesAsync();

        }
        
        private static IEnumerable<VehicleType> GetVehicleTypes()
        {
            var vehicleTypes = new List<VehicleType>();
        var now = DateTime.Now;

        var type = new VehicleType("Motorcycle", 1);
            type.Vehicles.Add(new ParkVehicle("ABC123", "BLUE", "Volvo", "V70", 4, now, 1));
            vehicleTypes.Add(type);
            type = new VehicleType("Computer Chair", 1);
        type.Vehicles.Add(new ParkVehicle("ABC123", "YELLOW", "Volvo", "V70", 4, now, 3));
        vehicleTypes.Add(type);
        type = new VehicleType("Tractor", 2);
        type.Vehicles.Add(new ParkVehicle("ABC123", "BLACK", "Volvo", "V70", 4, now, 5));
        vehicleTypes.Add(type);
        type = new VehicleType("Bus", 3);
        type.Vehicles.Add(new ParkVehicle("ABC123", "WHITE", "Volvo", "V70", 4, now, 6));
        vehicleTypes.Add(type);
        type = new VehicleType("Car", 1);
        type.Vehicles.Add(new ParkVehicle("ABC123", "GREEN", "Volvo", "V70", 4, now, 1));
        vehicleTypes.Add(type);
        type = new VehicleType("Truck", 2);
        type.Vehicles.Add(new ParkVehicle("ABC123", "ORANGE", "Volvo", "V70", 4, now, 4));
        vehicleTypes.Add(type);
        type = new VehicleType("Forklift", 1);
        type.Vehicles.Add(new ParkVehicle("ABC123", "BLUE", "Volvo", "V70", 4, now, 3));
        vehicleTypes.Add(type);








        return vehicleTypes;
        }
        private static IEnumerable<Member> GetMembers()
        {
            var members = new List<Member>();


            members.Add(new Member("Anna", "Bosson", "921010-1111", 22));
            members.Add(new Member("Bertil", "Bisson", "921010-1111", 22));
            members.Add(new Member("Ceasar", "Besson", "921010-1111", 22));
            members.Add(new Member("David", "Byson", "921010-1111", 22));
            members.Add(new Member("Erik", "Blison", "921010-1111", 22));
            members.Add(new Member("Fabian", "Bason", "921010-1111", 22));
            members.Add(new Member("Gunnila", "Böson", "921010-1111", 22));
            members.Add(new Member("Hanna", "Båson", "921010-1111", 22));
            members.Add(new Member("Ivan", "Bäson", "931010-1111", 22));
            members.Add(new Member("Jens", "Johansson", "9201010-1111", 22));




            return members;
        }
        private static IEnumerable<ParkingSpot> GetParkinSpots(IConfiguration configuration)
        {
            var parkSpot = new List<ParkingSpot>();
            int garageCapacity = 0;

            if (int.TryParse(configuration.GetSection("GarageCapacity").Value, out garageCapacity) is true)
            {
                for (int i = 0; i < garageCapacity; i++)
                {
                    parkSpot.Add(new ParkingSpot(i+1));

                }
            }



            return parkSpot;
        }
    }
}