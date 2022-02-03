using Garage2._0.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models
{
    public class StatisticsViewModel
    {
        [Display(Name = "Parked Vehicles")]
        public int AmountOfVehicles { get; set; }
        [Display(Name = "Amount Of Wheels")]
        public int AmountOfWheels { get; set; }

        [Display(Name = "Current Total Fees")]
        public int CurrentFees { get; set; }
        public List<VehicleTypeHelper> VehicleTypeAmount { get; set; }
    }
}
