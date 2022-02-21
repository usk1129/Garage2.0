#nullable disable
using Garage2._0.Data;
using Garage2._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0.Controllers
{
    public class MemberController : Controller
    {
        private readonly Garage2_0Context _context;

        public MemberController(Garage2_0Context context)
        {
            _context = context;
        }

        // GET: Member
        public async Task<IActionResult> Index()
        {
            return View(await _context.Member.ToListAsync());
        }

        // GET: Member/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .Include(m => m.Vehicles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Member/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Member/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member)
        {
            if (ModelState.IsValid)
            {
                string[] words = member.PersonNumber.Split('-','+');
                var today = DateTime.Today;
                int lastTwoDigitsOfYear = int.Parse(today.ToString("yy"));

                var age = 0;
                if (words[0].Length == 6)
                {
                    int ageTwoDigits = int.Parse(words[0].Substring(0, 2));
                    if (lastTwoDigitsOfYear < ageTwoDigits)
                        age = lastTwoDigitsOfYear + 100 - ageTwoDigits;
                    else
                        age = lastTwoDigitsOfYear - ageTwoDigits;
                }
                else
                    age = today.Year - int.Parse(words[0].Substring(0, 4));

                member.Age = age;

                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }
        // GET: Member/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Member/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Member.FindAsync(id);
            _context.Member.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.Id == id);
        }
        //public async Task<IActionResult> ShowSearchResults(IndexViewModel viewModel)
        //{
        //    var firstNames = string.IsNullOrWhiteSpace(viewModel.Owner) ?
        //                            _context.Member :
        //                            _context.Member.Where(m => m.FirstName.StartsWith(viewModel.Owner));

        //    var model = new IndexViewModel
        //    {
        //        FirstName = await firstNames.ToListAsync(),
        //    };

        //    return View(nameof(Index), model);
        //}
    }
}
