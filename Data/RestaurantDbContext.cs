using Labb1_ResturantBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1_ResturantBookingSystem.Data
{
    public class Labb1RestaurantDbContext : DbContext
    {
        public Labb1RestaurantDbContext(DbContextOptions<Labb1RestaurantDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Table> tables { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Menu> menus { get; set; }
    }
}
