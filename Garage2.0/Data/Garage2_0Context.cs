#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage2._0.Models;

namespace Garage2._0.Data
{
    public class Garage2_0Context : DbContext
    {
        public Garage2_0Context (DbContextOptions<Garage2_0Context> options)
            : base(options)
        {
        }

        public DbSet<Garage2._0.Models.ParkVehicle> ParkVehicle { get; set; }
        public DbSet<Garage2._0.Models.VehicleType> VehicleType { get; set; }

        public DbSet<Garage2._0.Models.Member> Member { get; set; }
    }
}
