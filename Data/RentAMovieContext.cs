using Microsoft.EntityFrameworkCore;
using RentAMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Data
{
    public class RentAMovieContext : DbContext
    {
        public RentAMovieContext(DbContextOptions<RentAMovieContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MembershipType> MembershipType { get; set; }
    }
}
