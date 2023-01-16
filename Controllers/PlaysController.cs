using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AchimDaiana_Theater.Data;
using AchimDaiana_Theater.Models;
using Microsoft.Data.SqlClient;
using static System.Reflection.Metadata.BlobBuilder;

namespace AchimDaiana_Theater.Controllers
{
    public class PlaysController : Controller
    {
        private readonly LibraryContext _context;

        public PlaysController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Plays
        public async Task<IActionResult> Index(string sortOrder,string currentFilter,string searchString,int? pageNumber)
        {
            var libraryContext = _context.Plays.Include(b => b.Writer);
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var plays = from b in _context.Plays
                        select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                plays = plays.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    plays = plays.OrderByDescending(b => b.Title);
                    break;
                case "Price":
                    plays = plays.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    plays = plays.OrderByDescending(b => b.Price);
                    break;
                default:
                    plays = plays.OrderBy(b => b.Title);
                    break;
            }
            
            int pageSize = 2;
            return View(await PaginatedList<Play>.CreateAsync(plays.AsNoTracking(), pageNumber ?? 1, pageSize));



        }

        // GET: Plays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Plays == null)
            {
                return NotFound();
            }

            var play = await _context.Plays
                 .Include(s => s.Reservations)
                 .ThenInclude(e => e.Customer)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(m => m.ID == id);
            if (play == null)
            {
                return NotFound();
            }

            return View(play);
        }

        // GET: Plays/Create
        public IActionResult Create()
        {
            ViewData["LastName"] = new SelectList(_context.Writers, "WriterID", "LastName");
            return View();
        }

        // POST: Plays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Genre,Price,WriterID")] Play play)
        {
            try
            {
                if (ModelState.IsValid)
                {
                _context.Add(play);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }

            return View(play);
        }

        // GET: Plays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Plays == null)
            {
                return NotFound();
            }

            var play = await _context.Plays.FindAsync(id);

            if (play == null)
            {
                return NotFound();
            }
            ViewData["LastName"] = new SelectList(_context.Writers, "WriterID", "LastName", play.WriterID);
            return View(play);
        }

        // POST: Plays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var playToUpdate = await _context.Plays.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Play>( playToUpdate, "", s => s.Writer, s => s.Title, s => s.Price, s =>s.Genre))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists");
                }
            }
            return View(playToUpdate);
        }

        // GET: Plays/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Plays == null)
            {
                return NotFound();
            }

            var play = await _context.Plays
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (play == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }


            return View(play);
        }

        // POST: Plays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var play = await _context.Plays.FindAsync(id);
            if (play == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Plays.Remove(play);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }

        }

        private bool PlayExists(int id)
        {
          return _context.Plays.Any(e => e.ID == id);
        }
    }
}
