using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Genre> Genre { get; set; }
        public IEnumerable<Movie> Movie { get; set; }
    }
}
