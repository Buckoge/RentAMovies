using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models
{
    public class Rental 
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Display(Name = "Movie name")]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public bool Status { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}
