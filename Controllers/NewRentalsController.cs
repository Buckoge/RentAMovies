/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentAMovies.Data;
using RentAMovies.Models;

namespace RentAMovies.Controllers
{
    public class NewRentalsController : Controller
    {
        private readonly RentAMovieContext _context;

        public NewRentalsController(RentAMovieContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateNewRentals(NewRental _newRental)
        {
            var customer = _context.Customers.Single(
                c => c.Id == _newRental.CustomerId);
            
            var movies = _context.Movies.Where(
                m => _newRental.MovieIDs.Contains(m.Id)).ToList();
            
            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("movie is not available.");

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
            return View(_newRental);
        }
    }
    
}*/