using RentAMovies.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models.ViewModels
{
    public class RentalManyMovieViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public VideoKlub VideoKlub { get; set; }
        public Movie Movie { get; set; }
        public List<string> CustomerList { get; set; }
        public List<string> MovieList { get; set; }
        public List<string> GenreList { get; set; }
        public string StatusMessage { get; set; }
    }
}
