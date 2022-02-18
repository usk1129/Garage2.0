namespace Garage2._0.Models
{
    public class VehicleType
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }

        public virtual ICollection<ParkVehicle> Vehicles {get; set;} = new List<ParkVehicle>();
    }
}
