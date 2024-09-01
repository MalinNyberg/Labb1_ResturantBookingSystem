using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;
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
        public async Task AddDishAsync(CreateMenuDto createMenuDto)
        {
            var menu = new Menu
            {
                NameOfDish = createMenuDto.NameOfDish,
                Price = createMenuDto.Price
                // IsAvailable will be set later by the admin
            };

            await _menuRepository.AddDishAsync(menu);
            await _menuRepository.SaveChangesAsync();
        }

        public async Task DeleteDishAsync(int id)
        {
            await _menuRepository.DeleteDishAsync(id);
            await _menuRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<MenuDto>> GetAllDishesAsync()
        {
            var dishes = await _menuRepository.GetAllDishesAsync();

            
            return dishes.Select(dish => new MenuDto
            {
                MenuId = dish.MenuId,
                NameOfDish = dish.NameOfDish,
                Price = dish.Price,
                IsAvailable = dish.IsAvailable
            });
        }

        public async Task<MenuDto> GetDishByIdAsync(int id)
        {
            var dish = await _menuRepository.GetDishByIdAsync(id);
            if (dish == null) return null;

            // Convert Menu entity to MenuDto
            return new MenuDto
            {
                MenuId = dish.MenuId,
                NameOfDish = dish.NameOfDish,
                Price = dish.Price,
                IsAvailable = dish.IsAvailable
            };
        }

        public async Task<bool> IsDishAvailableAsync(int id)
        {
            return await _menuRepository.IsDishAvailableAsync(id);
        }

        public async Task UpdateMenuAsync(int id, UpdateMenuDto updateMenuDto)
        {
            var menuDish = await _menuRepository.GetDishByIdAsync(id);
            if (menuDish == null)
            {
                throw new ArgumentException("The dish is not found.");
            }

            menuDish.NameOfDish = updateMenuDto.NameOfDish;
            menuDish.Price = updateMenuDto.Price;
            menuDish.IsAvailable = updateMenuDto.IsAvailable;

            await _menuRepository.UpdateMenuAsync(menuDish);
            await _menuRepository.SaveChangesAsync();

        }
    }  
}
