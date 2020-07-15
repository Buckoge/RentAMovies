using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentAMovies.Data;
using RentAMovies.Migrations;
using RentAMovies.Models;
using RentAMovies.Models.ViewModels;

namespace RentAMovies.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        
        private readonly ApplicationDbContext _context;

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
            return View(IndexVM);
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
