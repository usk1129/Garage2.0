using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage2._0.Services
{
    public interface IMemberSelectListService
    {

        Task<IEnumerable<SelectListItem>> GetMembersAsync();
    }
}