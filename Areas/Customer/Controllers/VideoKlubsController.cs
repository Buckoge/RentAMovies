using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentAMovies.Data;
using RentAMovies.Models;
using RentAMovies.Models.ViewModels;

namespace RentAMovies.Controllers
{
    [Area("Customer")]
    public class VideoKlubsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VideoKlubsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VideoKlubs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VideoKlub.Include(v => v.Customer).Include(v => v.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VideoKlubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoKlub = await _context.VideoKlub
                .Include(v => v.Customer)
                .Include(v => v.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoKlub == null)
            {
                return NotFound();
            }

            return View(videoKlub);
        }

        // GET: VideoKlubs/Create
        public async Task<IActionResult> Create()
        {   
           
            RentalManyMovieViewModel model = new RentalManyMovieViewModel
            {
                Movies = await _context.Movies.ToListAsync(),
                VideoKlub = new Models.VideoKlub(),
                MovieList = await _context.Movies.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync(),
                Customers = await _context.Customers.ToListAsync(),
                CustomerList = await _context.Customers.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()
            };

            return View(model);



        }

        // POST: VideoKlubs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalManyMovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesMovieCustomerExists = _context.VideoKlub.Include(p => p.Customer).Where(p => p.MovieId == model.VideoKlub.MovieId && p.CustomerId == model.VideoKlub.CustomerId);
                if (doesMovieCustomerExists.Count() > 0)
                {
                    //Error
                }
                else
                {
                    _context.VideoKlub.Add(model.VideoKlub);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
                RentalManyMovieViewModel modelVM = new RentalManyMovieViewModel
                {
                    Movies = await _context.Movies.ToListAsync(),
                    VideoKlub = new Models.VideoKlub(),
                    MovieList = await _context.Movies.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
                    Customers = await _context.Customers.ToListAsync(),
                    CustomerList = await _context.Customers.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync()
                };

                return View(modelVM);

            }

        // GET: VideoKlubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoKlub = await _context.VideoKlub.FindAsync(id);
            if (videoKlub == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", videoKlub.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", videoKlub.MovieId);
            return View(videoKlub);
        }

        // POST: VideoKlubs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateCreated,CustomerId,MovieId,Status,DateRented,DateReturned")] VideoKlub videoKlub)
        {
            if (id != videoKlub.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoKlub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoKlubExists(videoKlub.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", videoKlub.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", videoKlub.MovieId);
            return View(videoKlub);
        }

        // GET: VideoKlubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoKlub = await _context.VideoKlub
                .Include(v => v.Customer)
                .Include(v => v.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoKlub == null)
            {
                return NotFound();
            }

            return View(videoKlub);
        }

        // POST: VideoKlubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoKlub = await _context.VideoKlub.FindAsync(id);
            _context.VideoKlub.Remove(videoKlub);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoKlubExists(int id)
        {
            return _context.VideoKlub.Any(e => e.Id == id);
        }
    }
}
