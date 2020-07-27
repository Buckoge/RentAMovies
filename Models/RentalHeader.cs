using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models
{
    public class RentalHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }
        public DateTime? RentalComplited { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
    }
}
