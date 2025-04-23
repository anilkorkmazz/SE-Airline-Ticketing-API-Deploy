using Microsoft.EntityFrameworkCore;
using AirlineTicketingAPI.Models;

namespace AirlineTicketingAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
