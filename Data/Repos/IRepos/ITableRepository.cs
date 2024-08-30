using Labb1_ResturantBookingSystem.Models;

namespace Labb1_ResturantBookingSystem.Data.Repos.IRepos
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<Table> GetTableByIdAsync(int id);
        Task AddTableAsync(Table table);
        Task UpdateTableAsync(Table table);
        Task DeleteTableAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
