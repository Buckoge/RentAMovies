using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
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
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public RentalDetailsCart detailCart { get; set; }

        public RentalCartController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public async Task<IActionResult> SummaryPost()
        {           


            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var Status = await _context.RentalHeader.Where(c => c.UserId == claim.Value).ToListAsync();

            List<RentalHeader> FilterdStatus = Status.Where(p => p.Status.StartsWith("A")).ToList();

            if (FilterdStatus.Count != 0)
            {
                return RedirectToAction("Message", "RentalCart", new { id = detailCart.RentalHeader.Id });

            }
            else
            {
                detailCart.listCart2 = await _context.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value).ToListAsync();


                detailCart.RentalHeader.RentalDate = DateTime.Now;
                detailCart.RentalHeader.UserId = claim.Value;
                detailCart.RentalHeader.Status = SD.RentalprocesStatusAcitve;

                List<RentalDetails> RentalDetailsList = new List<RentalDetails>();
                _context.RentalHeader.Add(detailCart.RentalHeader);
                await _context.SaveChangesAsync();



                foreach (var item in detailCart.listCart2)
                {
                    item.Movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == item.MovieId);
                    RentalDetails RentalDetails = new RentalDetails
                    {
                        MovieId = item.MovieId,
                        RentalHeaderId = detailCart.RentalHeader.Id,
                        Status = true,
                        Count = detailCart.listCart2.Count()
                    };

                    _context.RentalDetails.Add(RentalDetails);

                }
                await _emailSender.SendEmailAsync(_context.Users.Where(u => u.Id == claim.Value).FirstOrDefault().Email, "RentAMovie - Rental created" + detailCart.RentalHeader.Id.ToString(), "Rental has been submitted successfully.");

                _context.ShoppingCart.RemoveRange(detailCart.listCart2);
                HttpContext.Session.SetInt32(SD.ssShoppingCartCount, 0);
                await _context.SaveChangesAsync();

                


                //return RedirectToAction("Index", "Home");
                return RedirectToAction("Confirm", "RentalOrder", new { id = detailCart.RentalHeader.Id });
            }

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

        public IActionResult Message()
        {
            return View();
        }
    }
}
