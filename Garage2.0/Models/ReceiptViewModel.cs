using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        public string VehicleType { get; set; }

        public string RegNumber { get; set; }


        public string Color { get; set; }


        public string Brand { get; set; }


        public string Model { get; set; }
        public string Owner { get; set; }


        public int Wheels { get; set; }
        [Display(Name = "Check-In Time")]
        public DateTime? CheckInTime { get; set; }
        [Display(Name = "Check-Out Time")]
        public DateTime CheckOutTime { get; set; }

        public int Price { get; set; }
        [Display(Name = "Duration")]
        [DisplayFormat(DataFormatString = @"{0:dd\:hh\:mm\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan? ParkedTime { get; set; }

    }
}
