using Labb1_ResturantBookingSystem.Models;

namespace Labb1_ResturantBookingSystem.Data.Repos.IRepos
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllDishesAsync();
        Task AddDishAsync(Menu menu);
        Task DeleteDishAsync(int id);

        Task<bool> SaveChangesAsync();
        Task<bool> IsDishAvailableAsync(int id);
        Task<Menu> GetDishByIdAsync(int id);
        Task UpdateMenuAsync(Menu menuDish);
    }
}
