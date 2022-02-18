using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage2._0.Services
{
    public interface IVehicleTypeSelectListService
    {

        Task<IEnumerable<SelectListItem>> GetActiveVehicleTypesAsync();
        Task<IEnumerable<SelectListItem>> GetAllVehicleTypesAsync();
    }
}