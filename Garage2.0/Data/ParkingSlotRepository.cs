using Garage2._0.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage2._0.Data
{
    public class ParkingSlotRepository
    {
        public List<int> Slots { get; set; } = new List<int>();

        public IEnumerable<SelectListItem> GetParkingSlots()
        {
            List<SelectListItem> parkingSlots = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };
            return parkingSlots;
        }

        public IEnumerable<SelectListItem> GetParkingSlots(int vehicleType)
        {
            List<SelectListItem> parkingSlots = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = "VALUE FROMHERE!!!",
                    Text = " SLOTTEXT"
                }
            };
            return parkingSlots;


        }
    }


}
