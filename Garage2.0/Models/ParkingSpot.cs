using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models
{
    public class ParkingSpot
    {
        [Display(Name = "Garage Slot")]
        public int Id { get; set; }

        public ICollection<ParkVehicle> ParkVehicles { get; set; }
    }
}
