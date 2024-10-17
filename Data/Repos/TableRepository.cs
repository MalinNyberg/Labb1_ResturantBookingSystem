using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1_ResturantBookingSystem.Data.Repos
{
    public class TableRepository : ITableRepository
    {
        private readonly Labb1RestaurantDbContext _context;

        public TableRepository(Labb1RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task AddTableAsync(Table table )
        {
            await _context.tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(int id)
        {
            var table = await _context.tables.FindAsync(id);
            if (table != null)
            {
                _context.tables.Remove(table);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _context.tables.ToListAsync();
        }

        public async Task<Table> GetTableByIdAsync(int id)
        {
            return await _context.tables.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) >= 0;
        }

        public async Task UpdateTableAsync(Table table)
        {
            _context.tables.Update(table);
            await _context.SaveChangesAsync();
        }

        public bool IsTableAvailable(int tableId, DateTime bookingTime)
        {
            // Kontrollera om bordet har en bokning som överlappar med den begärda tiden
            var isBooked = _context.bookings
                .Any(b => b.BookingId == tableId && b.Date == bookingTime);

            return !isBooked;  // Returnera true om bordet INTE är bokat
        }
    }
}
