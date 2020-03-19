using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Movie Movie{ get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
    }
}
