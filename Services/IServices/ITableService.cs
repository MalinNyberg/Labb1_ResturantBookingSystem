using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;

namespace Labb1_ResturantBookingSystem.Services.IServices
{
    public interface ITableService
    {
        Task<IEnumerable<TableDto>> GetAllTablesAsync();
        Task<TableDto> GetTableByIdAsync(int id);
        Task CreateTableAsync(CreateTableDto createTableDto);
        Task UpdateTableAsync(int id, CreateTableDto createTableDto);
        Task DeleteTableAsync(int id);
    }
}
