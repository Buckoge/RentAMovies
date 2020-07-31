using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models.ViewModels
{
    public class RentalDetailsViewModel
    {
        public RentalHeader RentalHeader { get; set; }
        public List<RentalDetails> RentalDetails { get; set; }
    }
}
