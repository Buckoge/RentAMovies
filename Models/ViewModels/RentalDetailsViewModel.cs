using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models.ViewModels
{
    public class RentalDetailsViewModel
    {
        public RentalHeader RentalHeader { get; set; }
        public List<RentalHeader> RentalHeaders { get; set; }
        public List<RentalDetails> RentalDetails { get; set; }
        public RentalDetails RentalDetails1 { get; set; }
    }
}
