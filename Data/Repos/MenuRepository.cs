using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Labb1_ResturantBookingSystem.Data.Repos
{
    public class MenuRepository : IMenuRepository
    {
        private readonly RestaurantDbContext _context;

        public MenuRepository(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task AddDishAsync(Menu menu)
        {
            await _context.menus.AddAsync(menu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDishAsync(int id)
        {
            var menuDish = await _context.menus.FindAsync(id);
            if (menuDish != null) 
            {
                _context.menus.Remove(menuDish);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Menu>> GetAllDishesAsync()
        {
            return await _context.menus.ToListAsync(); 
        }

        public async Task<Menu> GetDishByIdAsync(int id)
        {
            return await _context.menus.FindAsync(id);
        }

        public async Task<bool> IsDishAvailableAsync(int id)
        { 
            var dish = await _context.menus.FirstOrDefaultAsync(m => m.MenuId == id);
            return dish != null && dish.IsAvailable;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task UpdateMenuAsync(Menu menuDish)
        {
            _context.menus.Update(menuDish);
            await _context.SaveChangesAsync();
        }
    }
}
