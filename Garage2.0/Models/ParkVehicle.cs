using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models
{
    public class ParkVehicle
    {
        public int Id { get; set; }
        public VehicleType VehicleType { get; set; }

        [StringLength(30)]
        public string RegNumber { get; set; }

        [StringLength(30)]
        public string Color { get; set; }

        [StringLength(30)]
        public string Brand { get; set; }

        [StringLength(30)]
        public string Model { get; set; }

        [Range(0,10)]
        public int Wheels { get; set; }
        [Display(Name = "Check-In Time")]
        public DateTime  CheckInTime { get; set; }

        public DateTimeOffset Now { get; set; }

    }
}
