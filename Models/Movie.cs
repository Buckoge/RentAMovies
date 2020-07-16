using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]

        [Display(Name = "Genre name")]
        public int? GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
        public string? MovieDescription { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberInStock { get; set; }
        public int NumberAvailable { get; set; }
        public string? Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = " Price should be greater than ${1}")]
        public double Price { get; set; }
    }

}
