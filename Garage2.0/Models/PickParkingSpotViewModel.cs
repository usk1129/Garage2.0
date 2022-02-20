using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models
{
    public class PickParkingSpotViewModel
    {


        public int VehicleId { get; set; }
        public string RegNR { get; set; }
        public int Size { get; set; }
        public int ParkingSpotId { get; set; }

        //public IEnumerable<ParkingSpot> interval { get; set; } = new List<ParkingSpot>();
        public IEnumerable<SelectListItem> ParkingSpots { get; set; } = new List<SelectListItem>();
        public List<ParkingSpot> ListParkingSpots { get; set; } = new List<ParkingSpot>();
        public string? MemberName
        {
            get; set;
        }

        [Display(Name = "Check-In Time")]
        public DateTime CheckInTime
        {
            get; set;
        }
    }
}