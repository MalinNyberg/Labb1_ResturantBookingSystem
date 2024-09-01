using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;

namespace Labb1_ResturantBookingSystem.Services.IServices
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuDto>> GetAllDishesAsync();
        Task<MenuDto> GetDishByIdAsync(int id);
        Task AddDishAsync(CreateMenuDto createMenuDto);
        Task DeleteDishAsync(int id);
        Task UpdateMenuAsync(int id, UpdateMenuDto updateMenuDto);
        Task<bool> IsDishAvailableAsync(int id);
    }
}
