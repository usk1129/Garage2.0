﻿using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models
{
    public class ParkVehicle
    {
        public int Id { get; set; }
        // public VehicleType VehicleType { get; set; }

        //public string ParkingSlot { get; set; }
        //        public IEnumerable<SelectListItem> ParkingSlots { get; set; } 

        [Display(Name = "Registration Number")]

        [StringLength(30)]
        public string RegNumber { get; set; }

        [StringLength(30)]
        public string Color { get; set; }

        [StringLength(30)]
        public string Brand { get; set; }

        [StringLength(30)]
        public string Model { get; set; }

        [Range(0, 10)]
        public int Wheels { get; set; }
        [Display(Name = "Check-In Time")]
        public DateTime? CheckInTime { get; set; }

        //[ForeignKey("Member")]

        // Foreign Key
        public int MemberId { get; set; }

        // Nav Prop
        public Member? Member { get; set; }
        [Display(Name = "Vehicle Type")]

        public int VehicleTypeID { get; set; }

        public VehicleType? VehicleType { get; set; }

        public int? ParkingSpotId { get; set; }




        public ICollection<ParkingSpot> Parkings { get; set; } = new List<ParkingSpot>();


        public ParkVehicle()
        {
        }

        public ParkVehicle(string regNr, string color, string brand, string model, int wheels, DateTime? checkInTime, int memberId)
        {

            RegNumber = regNr;
            Color = color;
            Brand = brand;
            Model = model;
            Wheels = wheels;
            CheckInTime = checkInTime;
            MemberId = memberId;
            //VehicleTypeID = vehicleTypeId;


        }

    }
}
