using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models
{
    public class Member
    {
        public int Id { get; set; }

        public string? Avatar { get; set; }
        [Display(Name = "First Name")]

        public string FirstName { get; set; }
        [Display(Name = "Last Name")]

        public string LastName { get; set; }

        [Display(Name = "Person Number")]
        [CheckPersonNr]
        public string PersonNumber { get; set; }
        [Display(Name = "Age")]
        public int Age { get; set; }

        // Nav Prop
        public ICollection<ParkVehicle> Vehicles { get; set; } = new List<ParkVehicle>();
        public Member()
        {
        }

        public Member(string avatar, string firstName, string lastName, string personNumber, int age)
        {
            Avatar = avatar;
            FirstName = firstName;
            LastName = lastName;
            PersonNumber = personNumber;
            Age = age;
        }
        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
