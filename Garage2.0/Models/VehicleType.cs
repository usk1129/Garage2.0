namespace Garage2._0.Models
{
    public class VehicleType
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }

        public ICollection<ParkVehicle> Vehicles { get; set; } = new List<ParkVehicle>();


        public VehicleType()
        {
        }

        public VehicleType(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }
}
