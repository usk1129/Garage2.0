using Garage2._0.Data;
using Garage2._0.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0.Services
{
    public class MemberSelectListService : IMemberSelectListService
    {
        private readonly Garage2_0Context db;
        public MemberSelectListService(Garage2_0Context db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<SelectListItem>> GetMembersAsync()
        {
             return await db.Member
            .Select(v => v)
            .Select(t => new SelectListItem
            {
                Text = t.FirstName + " " + t.LastName,
                Value = t.Id.ToString()
            })
            .ToListAsync();
        }

    }
}
