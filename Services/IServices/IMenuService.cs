using Labb1_ResturantBookingSystem.Models;

namespace Labb1_ResturantBookingSystem.Services.IServices
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAllDishesAsync();
        Task<Menu> GetDishByIdAsync(int id);
        Task AddDishAsync(Menu menu);
        Task DeleteDishAsync(int id);
        Task UpdateMenuAsync(int id,Menu menu);
        Task<bool> IsDishAvailableAsync(int id);
    }
}
