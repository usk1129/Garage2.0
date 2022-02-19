using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models
{
    public class ParkingSpot
    {
        public ParkingSpot()
        {

        }
        public ParkingSpot(int v)
        {
            ParkingSpotNr = v;
        }

        [Display(Name = "Garage Slot")]
        public int Id { get; set; }
        public int ParkingSpotNr { get; set; }


        public int? ParkVehicleID { get; set; }

        public ParkVehicle? ParkVehicle { get; set; }
        public int V { get; }
    }
}
