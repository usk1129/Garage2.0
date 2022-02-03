using Garage2._0.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage2._0.Models
{
    public class StatisticsViewModel
    {
        public int AmountOfVehicles { get; set; }
        public int AmountOfWheels { get; set; }
        public int CurrentFees { get; set; }
        public List<VehicleTypeHelper> VehicleTypeAmount { get; set; }
    }
}
