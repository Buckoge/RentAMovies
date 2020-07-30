using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentAMovies.Data;
using RentAMovies.Migrations;
using RentAMovies.Models;
using RentAMovies.Models.ViewModels;
using RentAMovies.Utility;

namespace RentAMovies.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        [TempData]
        public string StatusMessage { get; set; }

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            Models.ViewModels.IndexViewModel IndexVM = new Models.ViewModels.IndexViewModel()

            {
                Genre = await _context.Genres.ToListAsync(),
                Movie = await _context.Movies.Include(g => g.Genre).ToListAsync()
            };

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                var cnt = _context.ShoppingCart.Where(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.ssShoppingCartCount, cnt);
            }

            return View(IndexVM);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var MovieFromDb = await _context.Movies.Include(m => m.Genre).Where(m => m.Id == id).FirstOrDefaultAsync();

            ShoppingCart cartObj = new ShoppingCart()
            {
                Movie = MovieFromDb,
                MovieId = MovieFromDb.Id
            };

            return View(cartObj);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ShoppingCart CartObject)
        {
            CartObject.Id = 0;
            //if (ModelState.IsValid)
            {
                

                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                CartObject.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDb = await _context.ShoppingCart.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId
                                                && c.MovieId == CartObject.MovieId).FirstOrDefaultAsync();

                //var IsMovieAlreadyExists = _context.ShoppingCart.Include(p => p.Movie).Where(p => p.MovieId == CartObject.MovieId);
                //var IsMovieAlreadyExists = _context.ShoppingCart.Include(p => p.MovieId).ToListAsync();
                //if (cartFromDb.MovieId == CartObject.MovieId )
                //{
                //    StatusMessage = "Error : Movie is already rented, Please choose another one.";
                //    // throw new ArgumentException();

                //}

                //else

                if (cartFromDb == null)
                {
                    await _context.ShoppingCart.AddAsync(CartObject);
                }
                else
                {
                    
                    cartFromDb.Count = cartFromDb.Count + CartObject.Count;
                }
                await _context.SaveChangesAsync();

                var count = _context.ShoppingCart.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId).ToList().Count();
                HttpContext.Session.SetInt32(SD.ssShoppingCartCount, count);

                return RedirectToAction("Index");
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
