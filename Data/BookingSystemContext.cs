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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "John Doe", PhoneNumber = "1234567890" },
                new Customer { Id = 2, Name = "Jane Smith", PhoneNumber = "0987654321" },
                new Customer { Id = 3, Name = "Malin Nyberg", PhoneNumber = "0701234567" },
                new Customer { Id = 4, Name = "Fredrik Jansson", PhoneNumber = "0707654321" }
            );

            // Seed MenuItems
            modelBuilder.Entity<Menu>().HasData(
                new Menu { MenuId = 1, NameOfDish = "Spaghetti Bolognese", Price = 12.99M },
                new Menu { MenuId = 2, NameOfDish = "Caesar Salad", Price = 9.99M },
                new Menu { MenuId = 3, NameOfDish = "Pizza Vesuvio", Price = 8.99M },
                new Menu { MenuId = 4, NameOfDish = "Swedish Meatballs - Italian style", Price = 10.99M }
            );

            // Seed Tables
            modelBuilder.Entity<Table>().HasData(
                new Table { TableId = 1, TableNumber = 1, NumberOfSeats = 4 },
                new Table { TableId = 2, TableNumber = 2, NumberOfSeats = 2 },
                new Table { TableId = 3, TableNumber = 3, NumberOfSeats = 6 },
                new Table { TableId = 4, TableNumber = 4, NumberOfSeats = 4 },
                new Table { TableId = 5, TableNumber = 5, NumberOfSeats = 8 }

            );

            // Seed Bookings
            modelBuilder.Entity<Booking>().HasData(
                new Booking { BookingId = 1, CustomerId = 1, TableId = 1, Date = DateTime.Now.AddDays(1), Time = new TimeSpan(18, 0, 0) },
                new Booking { BookingId = 2, CustomerId = 2, TableId = 2, Date = DateTime.Now.AddDays(2), Time = new TimeSpan(19, 0, 0) }
            );
        }

    }
}

//Data Source=(localdb)\.;Initial Catalog=BookingSystemDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=False