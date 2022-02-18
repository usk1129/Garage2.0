using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage2._0.Models
{
    public class IndexViewModel
    {

        public IEnumerable<ParkVehicle> ParkVehicles { get; set; } = new List<ParkVehicle>();
        //public IEnumerable<SelectListItem> VehicleTypes  { get; set; } = new List<SelectListItem>();


    }
}
