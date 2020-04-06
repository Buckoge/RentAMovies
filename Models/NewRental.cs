using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models
{
    public class NewRental
    {
        public int CustomerId { get; set; }
        public List<int> MovieIDs { get; set; }
    }
    
}
