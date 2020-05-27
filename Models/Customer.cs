using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models
{
    public class Customer
    {   [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        [Required]
        [Display(Name = "MembershipType")]
        public int MembershipTypeId { get; set; }
        public MembershipType MembershipType { get; set; }
        [Min18YearsIfAMember]
        public DateTime Birthdate { get; set; }
    }
}
