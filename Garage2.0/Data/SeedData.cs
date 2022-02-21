
using Bogus;
using Garage2._0.Data;
using Garage2._0.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Garage2._0.Data
{
    public class SeedData
    {
        private static Faker faker;
        public static async Task InitAsync(Garage2_0Context db, IConfiguration configuration)
        {
            if (await db.VehicleType.AnyAsync()) return;

            faker = new Faker("sv");
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


            for(int i = 0; i < 20; i++)
            {
                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                var PersonNumber = faker.Address.CountryCode();
                var age = faker.Random.Number(1, 100);
                var avatar = faker.Internet.Avatar();
                var member = new Member(avatar,fName, lName, PersonNumber, age);
                members.Add(member);
            }
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