
using Garage2._0.Data;
using Garage2._0.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Garage2._0.Data
{
    public class SeedData
    {

        public static async Task InitAsync(Garage2_0Context db)
        {
            if (await db.VehicleType.AnyAsync()) return;

            var vehicleTypes = GetVehicleTypes();
            var members = GetMembers();
            var vehicles = GetVehicles();

            await db.AddRangeAsync(members);
            await db.AddRangeAsync(vehicleTypes);
           // await db.AddRangeAsync(vehicles);


            await db.SaveChangesAsync();

        }

        private static object GetVehicles()
        {
            var vehicles = new List<ParkVehicle>();
            var now = DateTime.Now;

            vehicles.Add(new ParkVehicle("1", "ABC123", "BLUE", "Volvo", "V70", 4, now, 1, 2));
            vehicles.Add(new ParkVehicle("4", "ABC123", "RED", "Volvo", "V70", 4, now, 2, 1));
            vehicles.Add(new ParkVehicle("6", "ABC123", "YELLOW", "Volvo", "V70", 4, now, 3, 3));
            vehicles.Add(new ParkVehicle("7", "ABC123", "BLACK", "Volvo", "V70", 4, now, 5, 4));
            vehicles.Add(new ParkVehicle("1", "ABC123", "WHITE", "Volvo", "V70", 4, now, 6, 2));
            vehicles.Add(new ParkVehicle("8", "ABC123", "GREEN", "Volvo", "V70", 4, now, 1, 2));
            vehicles.Add(new ParkVehicle("9", "ABC123", "ORANGE", "Volvo", "V70", 4, now, 4, 2));
            vehicles.Add(new ParkVehicle("2", "ABC123", "BLUE" , "Volvo", "V70", 4, now, 3, 2));


                return vehicles;
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
    }
}