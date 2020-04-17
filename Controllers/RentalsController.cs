using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using RentAMovies.Data;
using RentAMovies.Models;

namespace RentAMovies.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            var applicationDbContext = _context.Rentals.Include(r => r.Customer).Include(r => r.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

    
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,CustomerId,MovieId,Status,DateRented,DateReturned")] int CustomerId)
        {
            var model = new Rental { DateCreated = DateTime.Now };
            
            //    _context.Add(rental);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));

            ViewData["Customer"] = _context.Customers.First(c => c.Id == CustomerId);
            ViewData["MoviesSelectList"] = new SelectList(_context.Movies, "Id", "Name");
            return View(model);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", rental.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", rental.MovieId);
            return View(rental);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateCreated,CustomerId,MovieId,Status,DateRented,DateReturned")] Rental rental)
        {
            if (id != rental.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", rental.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", rental.MovieId);
            return View(rental);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.Id == id);
        }
    }
}
