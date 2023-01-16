using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AchimDaiana_Theater.Data;
using AchimDaiana_Theater.Models;

namespace AchimDaiana_Theater.Controllers
{
    public class TheaterPlaysController : Controller
    {
        private readonly LibraryContext _context;

        public TheaterPlaysController(LibraryContext context)
        {
            _context = context;
        }

        // GET: TheaterPlays
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.TheaterPlays.Include(t => t.Play).Include(t => t.Theater);
            return View(await libraryContext.ToListAsync());
        }

        // GET: TheaterPlays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TheaterPlays == null)
            {
                return NotFound();
            }

            var theaterPlay = await _context.TheaterPlays
                .Include(t => t.Play)
                .Include(t => t.Theater)
                .FirstOrDefaultAsync(m => m.PlayID == id);
            if (theaterPlay == null)
            {
                return NotFound();
            }

            return View(theaterPlay);
        }

        // GET: TheaterPlays/Create
        public IActionResult Create()
        {
            ViewData["PlayID"] = new SelectList(_context.Plays, "ID", "ID");
            ViewData["TheaterID"] = new SelectList(_context.Theaters, "ID", "TheaterName");
            return View();
        }

        // POST: TheaterPlays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TheaterID,PlayID")] TheaterPlay theaterPlay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(theaterPlay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayID"] = new SelectList(_context.Plays, "ID", "ID", theaterPlay.PlayID);
            ViewData["TheaterID"] = new SelectList(_context.Theaters, "ID", "TheaterName", theaterPlay.TheaterID);
            return View(theaterPlay);
        }

        // GET: TheaterPlays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TheaterPlays == null)
            {
                return NotFound();
            }

            var theaterPlay = await _context.TheaterPlays.FindAsync(id);
            if (theaterPlay == null)
            {
                return NotFound();
            }
            ViewData["PlayID"] = new SelectList(_context.Plays, "ID", "ID", theaterPlay.PlayID);
            ViewData["TheaterID"] = new SelectList(_context.Theaters, "ID", "TheaterName", theaterPlay.TheaterID);
            return View(theaterPlay);
        }

        // POST: TheaterPlays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TheaterID,PlayID")] TheaterPlay theaterPlay)
        {
            if (id != theaterPlay.PlayID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theaterPlay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheaterPlayExists(theaterPlay.PlayID))
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
            ViewData["PlayID"] = new SelectList(_context.Plays, "ID", "ID", theaterPlay.PlayID);
            ViewData["TheaterID"] = new SelectList(_context.Theaters, "ID", "TheaterName", theaterPlay.TheaterID);
            return View(theaterPlay);
        }

        // GET: TheaterPlays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TheaterPlays == null)
            {
                return NotFound();
            }

            var theaterPlay = await _context.TheaterPlays
                .Include(t => t.Play)
                .Include(t => t.Theater)
                .FirstOrDefaultAsync(m => m.PlayID == id);
            if (theaterPlay == null)
            {
                return NotFound();
            }

            return View(theaterPlay);
        }

        // POST: TheaterPlays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TheaterPlays == null)
            {
                return Problem("Entity set 'LibraryContext.TheaterPlays'  is null.");
            }
            var theaterPlay = await _context.TheaterPlays.FindAsync(id);
            if (theaterPlay != null)
            {
                _context.TheaterPlays.Remove(theaterPlay);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheaterPlayExists(int id)
        {
          return _context.TheaterPlays.Any(e => e.PlayID == id);
        }
    }
}
