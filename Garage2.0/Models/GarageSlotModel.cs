using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models
{
    public class GarageSlotModel
    {

        [Display(Name = "Garage Capacity")]
        public int Capacity { get; set; }


        [Display(Name = "Garage Slot")]
        public int Slot { get; set; }

        [Display(Name = "Occupancy Status")]
        public string Occupancy { get; set; }


    }
}
