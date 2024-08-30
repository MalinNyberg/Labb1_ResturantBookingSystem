using Labb1_ResturantBookingSystem.Models;
using Microsoft.EntityFrameworkCore;


namespace Labb1_ResturantBookingSystem.Data
{
    public class BookingSystemContext : DbContext
    {
       public BookingSystemContext(DbContextOptions<BookingSystemContext> options) : base(options) 
       {
            
       }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Table> tables { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Menu> menus { get; set; }


    }
}

//Data Source=(localdb)\.;Initial Catalog=BookingSystemDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=False