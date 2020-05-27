/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentAMovies.Data;
using RentAMovies.Models;

namespace RentAMovies.Controllers
{
    public class NewRentalsController
    {
        private ApplicationDbContext _context;

        public NewRentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public Task<IActionResult> CreateNewRentals(NewRental newRental)
        {
           
                var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

                var movies = _context.Movies.Where(
                    m => newRental.MovieIDs.Contains(m.Id)).ToList();

                foreach (var movie in movies)
                {
                    // if (movie.NumberAvailable == 0)
                    //      return BadRequestResult("Movie is not available.");

                    movie.NumberAvailable--;

                    var rental = new Rental
                    {
                        Customer = customer,
                        Movie = movie,
                        DateRented = DateTime.Now
                    };

                    _context.Rentals.Add(rental);
                }

                _context.SaveChanges();
            
                return OkResult(newRental);
            
        }
    }
}*/