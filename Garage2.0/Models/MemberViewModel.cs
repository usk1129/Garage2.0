namespace Garage2._0.Models
{
    public class MemberViewModel
    {
        public string? Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonNumber { get; set; }
        public int Age { get; set; }

        public ICollection<ParkVehicle> Vehicles { get; set; } = new List<ParkVehicle>();
    }
}
