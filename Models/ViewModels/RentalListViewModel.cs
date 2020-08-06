using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models.ViewModels
{
    public class RentalListViewModel
    {
        public IList<RentalDetailsViewModel> Rentals { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
