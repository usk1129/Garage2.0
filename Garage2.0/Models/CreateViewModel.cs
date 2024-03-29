﻿using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models
{
    public class CreateViewModel
    {
        //public int Id { get; set; }
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
        public DateTime CheckInTime { get; set; }

        //[ForeignKey("Member")]

        // Foreign Key
        public int MemberId { get; set; }

        [Display(Name = "Vehicle Type")]
        public int VehicleTypeID { get; set; }
        public int ParkingSpotID { get; set; }

    }
}
