using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1_ResturantBookingSystem.Data.Repos
{
    public class BookingRepository : IBookingRepository
    {
        private readonly RestaurantDbContext _context;

        public BookingRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.bookings.ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _context.bookings.FirstOrDefaultAsync(b => b.BookingId == id);
        }

        //public async Task<Booking> GetBookingByIdAsync(int id) 
        //{
        //    return await _context.bookings.FindAsync(id);
        //}

        public async Task AddBookingAsync(Booking booking)
        {
            await _context.bookings.AddAsync(booking);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.bookings.Update(booking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _context.bookings.FindAsync(id);
            if (booking != null)
            {
                _context.bookings.Remove(booking);
            }
        }
        public async Task SaveChangesAsync() 
        {
            await _context.SaveChangesAsync();
        }
    }
}
