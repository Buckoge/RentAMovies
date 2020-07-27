using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models.ViewModels
{
    public class CustomerAndMovieViewModel
    {
        public List<Movie> Movies { get; set; }
        public IEnumerable<VideoKlub> VideoKlubs { get; set; }
        public Customer Customer { get; set; }
        public VideoKlub VideoKlub { get; set; }
    }
}
