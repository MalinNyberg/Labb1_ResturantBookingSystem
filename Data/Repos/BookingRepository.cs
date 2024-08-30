using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1_ResturantBookingSystem.Data.Repos
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingSystemContext _context;

        public BookingRepository(BookingSystemContext context)
        {
            _context = context;
        }
        public async Task AddBookingAsync(Booking booking)
        {
            await _context.bookings.AddAsync(booking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _context.bookings.FindAsync(id);
            if (booking != null)
            {
                _context.bookings.Remove(booking);
            }
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
           return await _context.bookings.Include(b => b.Table).ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _context.bookings.Include(b => b.Table).FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Task UpdateBookingAsync(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
