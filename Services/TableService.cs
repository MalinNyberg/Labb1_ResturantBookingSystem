using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Services.IServices;

namespace Labb1_ResturantBookingSystem.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;

        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }
        public async Task CreateTableAsync(Table table)
        {
            await _tableRepository.AddTableAsync(table);
            await _tableRepository.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(int id)
        {
            await _tableRepository.DeleteTableAsync(id);
            await _tableRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _tableRepository.GetAllTablesAsync();
        }

        public async Task<Table> GetTableByIdAsync(int id)
        {
            return await _tableRepository.GetTableByIdAsync(id);
        }

        public async Task UpdateTableAsync(int id, Table table)
        {
            var existingTable = await _tableRepository.GetTableByIdAsync(id);
            if (existingTable == null)
            {
                throw new ArgumentException("Table not found.");
            }

            existingTable.NumberOfSeats = table.NumberOfSeats;


            await _tableRepository.UpdateTableAsync(existingTable);
            await _tableRepository.SaveChangesAsync();
        }
    }
}
