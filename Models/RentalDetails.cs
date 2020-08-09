using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models
{
    public class RentalDetails
    {
        //public RentalDetails()
        //{
        //    Status = true;
        //}

        [Key]
        public int Id { get; set; }

        [Required]
        public int RentalHeaderId { get; set; }

        [ForeignKey("RentalHeaderId")]
        public virtual RentalHeader RentalHeader { get; set; }

        [Required]
        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
        public int Count { get; set; }
        public bool Status { get; set; }
    }
}
