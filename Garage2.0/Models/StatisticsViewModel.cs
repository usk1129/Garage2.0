using Garage2._0.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models
{
    public class StatisticsViewModel
    {

        [Display(Name = "Members")]
        public int Members { get; set; }
        [Display(Name = "Total Vehicles")]
        public int AmountOfVehicles { get; set; }
        [Display(Name = "Amount Of Wheels")]
        public int AmountOfWheels { get; set; }


        [Display(Name = "Parked Vehicles")]
        public int AmountOfParkedVehicles { get; set; }

        [Display(Name = "Current Total Fees")]
        public int CurrentFees { get; set; }

        [Display(Name = "Total Fees Next Hour")]
        public int PredictedFees { get; set; }
        
        public List<VehicleTypeHelper> VehicleTypeAmount { get; set; }
    }
}
