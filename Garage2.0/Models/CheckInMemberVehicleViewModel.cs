﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models
{
    public class CheckInMemberVehicleViewModel
    {
            public string? MemberName { get; set; }

        public IEnumerable<SelectListItem> Vehicles  { get; set; } = new List<SelectListItem>();
        public int VehicleId { get; set; }
        public int? MemberId { get; set; }

        [Display(Name = "Check-In Time")]
        public DateTime CheckInTime { get; set; }
    }


}