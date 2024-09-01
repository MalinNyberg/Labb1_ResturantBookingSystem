using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;
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

        public async Task CreateTableAsync(CreateTableDto createTableDto)
        {
            var table = new Table
            {
                NumberOfSeats = createTableDto.NumberOfSeats,
                TableNumber = createTableDto.TableNumber
            };

            await _tableRepository.AddTableAsync(table);
            await _tableRepository.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(int id)
        {
            await _tableRepository.DeleteTableAsync(id);
            await _tableRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TableDto>> GetAllTablesAsync()
        {
            var tables = await _tableRepository.GetAllTablesAsync();

            
            return tables.Select(t => new TableDto
            {
                TableId = t.TableId,
                NumberOfSeats = t.NumberOfSeats,
                TableNumber = t.TableNumber
            });
        }

        public async Task<TableDto> GetTableByIdAsync(int id)
        {
            var table = await _tableRepository.GetTableByIdAsync(id);
            if (table == null) return null;

            
            return new TableDto
            {
                TableId = table.TableId,
                NumberOfSeats = table.NumberOfSeats,
                TableNumber = table.TableNumber
            };
        }

        public async Task UpdateTableAsync(int id, CreateTableDto updateTableDto)
        {
            var existingTable = await _tableRepository.GetTableByIdAsync(id);
            if (existingTable == null)
            {
                throw new ArgumentException("Table not found.");
            }

            existingTable.NumberOfSeats = updateTableDto.NumberOfSeats;
            existingTable.TableNumber = updateTableDto.TableNumber;

            await _tableRepository.UpdateTableAsync(existingTable);
            await _tableRepository.SaveChangesAsync();
        }
    }
}
