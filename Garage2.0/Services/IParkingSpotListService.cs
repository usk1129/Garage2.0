using Garage2._0.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage2._0.Services
{
    public interface IParkingSpotSelectListService
    {

        Task<IEnumerable<SelectListItem>> GetParkingSpotsAsync();
    }
}