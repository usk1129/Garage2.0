namespace Garage2._0.Models
{
    public class IndexViewModel
    {

        //public IEnumerable<ParkVehicle> ParkVehicles { get; set; } = new List<ParkVehicle>();
        //public IEnumerable<SelectListItem> VehicleTypes  { get; set; } = new List<SelectListItem>();

        public string RegNumber { get; set; }


        public string Color { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Wheels { get; set; }

        public int ParkingSpotNR { get; set; }

        public string Owner { get; set; }

        public string VehicleType { get; set; }
        public TimeSpan ParkTime { get; set; }
    }
}
