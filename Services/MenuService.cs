using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Services.IServices;

namespace Labb1_ResturantBookingSystem.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task AddDishAsync(Menu menu)
        {
            await _menuRepository.AddDishAsync(menu);
            await _menuRepository.SaveChangesAsync();
        }

        public async Task DeleteDishAsync(int id)
        {
            await _menuRepository.DeleteDishAsync(id);
            await _menuRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Menu>> GetAllDishesAsync()
        {
            return await _menuRepository.GetAllDishesAsync();
        }

        public async Task<Menu> GetDishByIdAsync(int id)
        {
            return await _menuRepository.GetDishByIdAsync(id);
        }

        public async Task<bool> IsDishAvailableAsync(int id)
        {
            return await _menuRepository.IsDishAvailableAsync(id);
        }

        public async Task UpdateMenuAsync(int id, Menu updatedMenuDish)
        {
            var menuDish = await _menuRepository.GetDishByIdAsync(id);
            if (menuDish == null)
            {
                throw new ArgumentException("The dish is not found.");
            }

            menuDish.NameOfDish = updatedMenuDish.NameOfDish;
            menuDish.Price = updatedMenuDish.Price;
            
            await _menuRepository.UpdateMenuAsync(menuDish);
            await _menuRepository.SaveChangesAsync();

        }
    }  
}
