using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AchimDaiana_Theater.Data;
using AchimDaiana_Theater.Models;
using AchimDaiana_Theater.Models.LibraryViewModels;
using System.Security.Policy;

namespace AchimDaiana_Theater.Controllers
{
    public class TheatersController : Controller
    {
        private readonly LibraryContext _context;

        public TheatersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Theaters
        public async Task<IActionResult> Index(int? id, int? playID)
        {
            var viewModel = new TheaterIndexData();
            viewModel.Theaters = await _context.Theaters
            .Include(i => i.TheaterPlays)
            .ThenInclude(i => i.Play)
            .ThenInclude(i => i.Reservations)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.TheaterName)
            .ToListAsync();

            if (id != null)
            {
                ViewData["TheaterID"] = id.Value;
                Theater theater = viewModel.Theaters.Where(
                i => i.ID == id.Value).Single();
                viewModel.Plays = theater.TheaterPlays.Select(s => s.Play);
            }
            if (playID != null)
            {
                ViewData["PlayID"] = playID.Value;
                viewModel.Reservations = viewModel.Plays.Where(
                x => x.ID == playID).Single().Reservations;
            }
            return View(viewModel);
        }

        // GET: Theaters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Theaters == null)
            {
                return NotFound();
            }

            var theater = await _context.Theaters
                .FirstOrDefaultAsync(m => m.ID == id);
            if (theater == null)
            {
                return NotFound();
            }

            return View(theater);
        }

        // GET: Theaters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Theaters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TheaterName,Location")] Theater theater)
        {
            if (ModelState.IsValid)
            {
                _context.Add(theater);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(theater);
        }

        // GET: Theaters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var theater = await _context.Theaters
            .Include(i => i.TheaterPlays).ThenInclude(i => i.Play)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (theater == null)
            {
                return NotFound();
            }
            PopulateTheaterPlayData(theater);
            return View(theater);

        }
        private void PopulateTheaterPlayData(Theater theater)
        {
            var allPlays = _context.Plays;
            var theaterPlays = new HashSet<int>(theater.TheaterPlays.Select(c => c.PlayID));
            var viewModel = new List<TheaterPlayData>();
            foreach (var play in allPlays)
            {
                viewModel.Add(new TheaterPlayData
                {
                    PlayID = play.ID,
                    Title = play.Title,
                    IsReleased = theaterPlays.Contains(play.ID)
                });
            }
            ViewData["Plays"] = viewModel;
        }

        // POST: Theaters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedPlays)
        {
            if (id == null)
            {
                return NotFound();
            }
            var theaterToUpdate = await _context.Theaters
            .Include(i => i.TheaterPlays)
            .ThenInclude(i => i.Play)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Theater>(
            theaterToUpdate,
            "",
            i => i.TheaterName, i => i.Location))
            {
                UpdateTheaterPlay(selectedPlays, theaterToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateTheaterPlay(selectedPlays, theaterToUpdate);
            PopulateTheaterPlayData(theaterToUpdate);
            return View(theaterToUpdate);
        }
        private void UpdateTheaterPlay(string[] selectedPlays, Theater theaterToUpdate)
        {
            if (selectedPlays == null)
            {
                theaterToUpdate.TheaterPlays = new List<TheaterPlay>();
                return;
            }
            var selectedPlaysHS = new HashSet<string>(selectedPlays);
            var theaterPlays = new HashSet<int>
            (theaterToUpdate.TheaterPlays.Select(c => c.Play.ID));
            foreach (var play in _context.Plays)
            {
                if (selectedPlaysHS.Contains(play.ID.ToString()))
                {
                    if (!theaterPlays.Contains(play.ID))
                    {
                        theaterToUpdate.TheaterPlays.Add(new TheaterPlay
                        {
                            TheaterID =
                       theaterToUpdate.ID,
                            PlayID = play.ID
                        });
                    }
                }
                else
                {
                    if (theaterPlays.Contains(play.ID))
                    {
                        TheaterPlay playToRemove = theaterToUpdate.TheaterPlays.FirstOrDefault(i
                       => i.PlayID == play.ID);
                        _context.Remove(playToRemove);
                    }
                }
            }
        }

        // GET: Theaters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Theaters == null)
            {
                return NotFound();
            }

            var theater = await _context.Theaters
                .FirstOrDefaultAsync(m => m.ID == id);
            if (theater == null)
            {
                return NotFound();
            }

            return View(theater);
        }

        // POST: Theaters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Theaters == null)
            {
                return Problem("Entity set 'LibraryContext.Theaters'  is null.");
            }
            var theater = await _context.Theaters.FindAsync(id);
            if (theater != null)
            {
                _context.Theaters.Remove(theater);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheaterExists(int id)
        {
          return _context.Theaters.Any(e => e.ID == id);
        }
    }
}
