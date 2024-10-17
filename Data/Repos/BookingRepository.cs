using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1_ResturantBookingSystem.Data.Repos
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Labb1RestaurantDbContext _context;

        public BookingRepository(Labb1RestaurantDbContext context)
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
            await _context.SaveChangesAsync();

        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsTableAvailableAsync(int tableId, DateTime date)
        {
            return await _context.bookings.AnyAsync(b => b.TableId == tableId && b.Date == date);
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _context.bookings.FindAsync(id);
            if (booking != null)
            {
                _context.bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }


        }
        public async Task SaveChangesAsync() 
        {
            await _context.SaveChangesAsync();
        }
    }
}
