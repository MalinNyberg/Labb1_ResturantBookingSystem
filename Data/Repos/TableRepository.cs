using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1_ResturantBookingSystem.Data.Repos
{
    public class TableRepository : ITableRepository
    {
        private readonly RestaurantDbContext _context;

        public TableRepository(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task AddTableAsync(Table table )
        {
            await _context.tables.AddAsync(table);
        }

        public async Task DeleteTableAsync(int id)
        {
            var table = await _context.tables.FindAsync(id);
            if (table != null)
            {
                _context.tables.Remove(table);
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
            await Task.CompletedTask;
        }
    }
}
