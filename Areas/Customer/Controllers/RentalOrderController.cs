using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAMovies.Data;
using RentAMovies.Models.ViewModels;

namespace RentAMovies.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class RentalOrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RentalOrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public async Task<IActionResult> Confirm(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            RentalDetailsViewModel rentalDetailsViewModel = new RentalDetailsViewModel()
            {
                RentalHeader = await _db.RentalHeader.Include(o => o.ApplicationUser).FirstOrDefaultAsync(o => o.Id == id && o.UserId == claim.Value),
                RentalDetails = await _db.RentalDetails.Where(o => o.Id == id).ToListAsync()
            };

            return View(rentalDetailsViewModel);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
