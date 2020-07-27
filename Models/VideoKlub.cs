using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models
{
    public class VideoKlub
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

        [Display(Name = "Customer name")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Display(Name = "Genre name")]
        public int? GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
         
        [Display(Name = "Movie name")]
        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        public bool Status { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }

    }
}
