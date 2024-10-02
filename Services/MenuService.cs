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

        // Metod för att lägga till en ny rätt i menyn.
        public async Task AddDishAsync(CreateMenuDto createMenuDto)
        {
            // Skapar en ny menyobjekt baserat på de data som skickas in via CreateMenuDto.
            var menu = new Menu
            {
                NameOfDish = createMenuDto.NameOfDish, 
                Price = createMenuDto.Price            
                                                       
            };

            
            await _menuRepository.AddDishAsync(menu);
            await _menuRepository.SaveChangesAsync();
        }

        // Metod för att ta bort en rätt från menyn baserat på ID.
        public async Task DeleteDishAsync(int id)
        {
            // Tar bort rätten och sparar ändringarna.
            await _menuRepository.DeleteDishAsync(id);
            await _menuRepository.SaveChangesAsync();
        }

        // Metod för att hämta alla rätter från menyn.
        public async Task<IEnumerable<MenuDto>> GetAllDishesAsync()
        {
            // Hämtar alla rätter
            var dishes = await _menuRepository.GetAllDishesAsync();

            // Konverterar varje Menu-entitet till MenuDto och returnerar en lista av dessa.
            return dishes.Select(dish => new MenuDto
            {
                MenuId = dish.MenuId,           
                NameOfDish = dish.NameOfDish,   
                Price = dish.Price,             
                IsAvailable = dish.IsAvailable  
            });
        }

        // Metod för att hämta en specifik rätt baserat på dess ID.
        public async Task<MenuDto> GetDishByIdAsync(int id)
        {
            // Hämtar rätten baserat på ID
            var dish = await _menuRepository.GetDishByIdAsync(id);
            if (dish == null) return null; // Returnerar null om rätten inte hittas.

            // Konverterar Menu-entiteten till en MenuDto och returnerar den.
            return new MenuDto
            {
                MenuId = dish.MenuId,           
                NameOfDish = dish.NameOfDish,   
                Price = dish.Price,             
                IsAvailable = dish.IsAvailable  
            };
        }

        // Metod för att kontrollera om en rätt är tillgänglig baserat på ID.
        public async Task<bool> IsDishAvailableAsync(int id)
        {
            // Kontrollerar om rätten är tillgängling
            return await _menuRepository.IsDishAvailableAsync(id);
        }

        // Metod för att uppdatera en befintlig rätt i menyn.
        public async Task UpdateMenuAsync(int id, UpdateMenuDto updateMenuDto)
        {
            // Hämtar den befintliga rätten baserat på ID
            var menuDish = await _menuRepository.GetDishByIdAsync(id);
            if (menuDish == null)
            {
                // undantag om rätten inte hittas.
                throw new ArgumentException("Rätten hittades inte.");
            }

            // Uppdaterar rättens information med den nya informationen.
            menuDish.NameOfDish = updateMenuDto.NameOfDish;  
            menuDish.Price = updateMenuDto.Price;             
            menuDish.IsAvailable = updateMenuDto.IsAvailable; 

            
            await _menuRepository.UpdateMenuAsync(menuDish);
            await _menuRepository.SaveChangesAsync();
        }


    }  
}
