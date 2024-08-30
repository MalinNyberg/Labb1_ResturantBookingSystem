using Labb1_ResturantBookingSystem.Models;

namespace Labb1_ResturantBookingSystem.Services.IServices
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<Table> GetTableByIdAsync(int id);
        Task CreateTableAsync(Table table);
        Task UpdateTableAsync(int id, Table table);
        Task DeleteTableAsync(int id);
    }
}
