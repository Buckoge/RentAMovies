using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentAMovies.Data;
using RentAMovies.Extensions;
using RentAMovies.Migrations;
using RentAMovies.Models;
using RentAMovies.Models.ViewModels;
using RentAMovies.Utility;

namespace RentAMovies.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class RentalOrderController : Controller
    {
        [BindProperty]
        public RentalDetailsViewModel rentalstatus { get; set; }

        private readonly ApplicationDbContext _db;
        private int PageSize = 5;
        private readonly IEmailSender _emailSender;

        public RentalOrderController(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
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

        [Authorize]
        public async Task<IActionResult> RentalHistory(int productPage = 1)
        {
            

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            RentalListViewModel rentalListVM = new RentalListViewModel()
            {
                Rentals = new List<RentalDetailsViewModel>(),
                RentalDetails = new List<RentalDetailsViewModel>()
                
            };


            List<RentalHeader> RentalHeaderList = await _db.RentalHeader.Include(o => o.ApplicationUser).Where(u => u.UserId == claim.Value).ToListAsync();

            foreach (RentalHeader item in RentalHeaderList)
            {
                RentalDetailsViewModel individual = new RentalDetailsViewModel
                {
                    RentalHeader = item,
                    RentalDetails = await _db.RentalDetails.Include(m => m.Movie).Where(o => o.RentalHeaderId == item.Id).ToListAsync()
                    
                };
                rentalListVM.Rentals.Add(individual);
            }
            
            var count = rentalListVM.Rentals.Count;
            rentalListVM.Rentals = rentalListVM.Rentals.OrderByDescending(p => p.RentalHeader.Id)
                                 .Skip((productPage - 1) * PageSize)
                                 .Take(PageSize).ToList();

            rentalListVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItem = count,
                urlParam = "/Customer/RentalOrder/RentalHistory?productPage=:"
            };

            return View(rentalListVM);
        }

        public async Task<IActionResult> GetRentalDetails(int Id)
        {            
            RentalDetailsViewModel RentalDetailsViewModel = new RentalDetailsViewModel()
            {
                RentalHeader = await _db.RentalHeader.Include(el => el.ApplicationUser).FirstOrDefaultAsync(m => m.Id == Id),
                RentalDetails = await _db.RentalDetails.Include(m => m.Movie).Where(m => m.RentalHeaderId == Id).ToListAsync()
            };
           
            return View(RentalDetailsViewModel);
        }

        public async Task<IActionResult> FinishRental(int Id)
        {
            RentalHeader rentalHeader = await _db.RentalHeader.FindAsync(Id);
            var FinishRental = await _db.RentalHeader.FirstOrDefaultAsync(c => c.Id == Id);
            FinishRental.Status = SD.RentalprocesStatusCompleted;
            await _db.SaveChangesAsync();

            await _emailSender.SendEmailAsync(_db.Users.Where(u => u.Id == rentalHeader.UserId).FirstOrDefault().Email, "RentAMovie - Rental finish " + rentalHeader.Id.ToString(), "Rental has been comleted successfully.");
            return RedirectToAction("RentalHistory");
        }

        public async Task<IActionResult> DeleteRental(int Id)
        {
            var RentHeader = await _db.RentalHeader.FirstOrDefaultAsync(c => c.Id == Id);

            _db.RentalHeader.Remove(RentHeader);
            await _db.SaveChangesAsync();

            return RedirectToAction("RentalHistory");
        }
        
        public async Task<IActionResult> Minus(int rentalId, int Id)
        {

            var MovieStatus = await _db.RentalDetails.FirstOrDefaultAsync(c => c.Id == Id);
            MovieStatus.Status = false;
            await _db.SaveChangesAsync();

            RentalListViewModel RentalListVM = new RentalListViewModel()
            {
                Rentals = new List<RentalDetailsViewModel>()
            };

            List<RentalDetails> RentalDetailsList = await _db.RentalDetails.ToListAsync();

            var Status = await _db.RentalDetails.Where(m => m.RentalHeaderId == rentalId).ToListAsync();
            var StatusList = Status.Where(p => p.Status).ToList();

            if (StatusList.Count == 0)
                {
                    RentalHeader rentalHeader = await _db.RentalHeader.FindAsync(rentalId);
                    rentalHeader.Status = SD.RentalprocesStatusCompleted;
                    await _db.SaveChangesAsync();

                }

            return RedirectToAction("GetRentalDetails", new { id = MovieStatus.RentalHeaderId });


        }
        
        public async Task<IActionResult> Plus(int rentalId, int Id)
        {
            var MovieStatus = await _db.RentalDetails.FirstOrDefaultAsync(c => c.Id == Id);
            MovieStatus.Status = true;
            await _db.SaveChangesAsync();

            RentalListViewModel RentalListVM = new RentalListViewModel()
            {
                Rentals = new List<RentalDetailsViewModel>()
            };

            var Status = await _db.RentalDetails.Where(m => m.RentalHeaderId == rentalId).ToListAsync();
            var StatusList = Status.Where(p => p.Status).ToList();

            if (StatusList.Count != 0)
            {
                RentalHeader rentalHeader = await _db.RentalHeader.FindAsync(rentalId);
                rentalHeader.Status = SD.RentalprocesStatusAcitve;
                await _db.SaveChangesAsync();

            }

            List<RentalDetails> RentalDetailsList = await _db.RentalDetails.ToListAsync();
           
            
            
            return RedirectToAction("GetRentalDetails", new { id = MovieStatus.RentalHeaderId });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
