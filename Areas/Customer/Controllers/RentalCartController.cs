using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAMovies.Data;
using RentAMovies.Models;
using RentAMovies.Models.ViewModels;
using RentAMovies.Utility;

namespace RentAMovies.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class RentalCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public RentalDetailsCart detailCart { get; set; }

        public RentalCartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            detailCart = new RentalDetailsCart()
            {
                RentalHeader = new Models.RentalHeader()
            };

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var cart = _context.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value);
            if (cart != null)
            {
                detailCart.listCart2 = cart.ToList();
            }

            foreach (var list in detailCart.listCart2)
            {
                list.Movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == list.MovieId);
                list.Movie.MovieDescription = SD.ConvertToRawHtml(list.Movie.MovieDescription);
                if (list.Movie.MovieDescription.Length > 100)
                {
                    list.Movie.MovieDescription = list.Movie.MovieDescription.Substring(0, 99) + "...";
                }
            }
            return View(detailCart);
        }



        public async Task<IActionResult> Remove(int cartId)
        {
            var cart = await _context.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartId);

            _context.ShoppingCart.Remove(cart);
            await _context.SaveChangesAsync();

            var cnt = _context.ShoppingCart.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.ssShoppingCartCount, cnt);


            return RedirectToAction(nameof(Index));
        }
    }
}
