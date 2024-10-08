using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;
using Labb1_ResturantBookingSystem.Services;
using Labb1_ResturantBookingSystem.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Labb1_ResturantBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenusController : ControllerBase
    {
        // Dependency injection
        private readonly IMenuService _menuService;

        
        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // GET-metod för att hämta alla rätter.
        [HttpGet("/GetAllDishes")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetAllDishesAsync()
        {
            // Hämtar alla rätter
            var dishes = await _menuService.GetAllDishesAsync();
            // Returnerar rätterna med statuskod 200 (OK).
            return Ok(dishes);
        }

        // GET-metod för att hämta en specifik rätt baserat på ID.
        [HttpGet("/GetDish/{id}")]
        public async Task<ActionResult<MenuDto>> GetDishByIdAsync(int id)
        {
            // Hämtar en rätt baserat på angivet ID.
            var dish = await _menuService.GetDishByIdAsync(id);

            // Om rätten inte finns, returnera statuskod 404 (Not Found).
            if (dish == null)
            {
                return NotFound();
            }
            // Returnerar rätten med statuskod 200 (OK).
            return Ok(dish);
        }

        //  GET-metod för att kontrollera om en rätt är tillgänglig.
        [HttpGet("/IsDishAvailable/{id}")]
        public async Task<ActionResult<bool>> IsDishAvailableAsync(int id)
        {
            // Kontrollerar om rätten är tillgänglig
            var isAvailable = await _menuService.IsDishAvailableAsync(id);
            // Returnerar resultatet (sant eller falskt) med statuskod 200 (OK).
            return Ok(isAvailable);
        }

        // POST-metod för att lägga till en ny rätt.
        [HttpPost("/AddDish")]
        public async Task<ActionResult<MenuDto>> AddDishAsync(MenuDto menuDto)
        {
            try
            {
                // Lägg till rätten och få det genererade ID:et
               await _menuService.AddDishAsync(menuDto);

                // Använd det genererade ID:et i CreatedAtAction
                return Ok("Dish created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT-metod för att uppdatera en befintlig rätt.
        [HttpPut("/UpdateDish")]
        public async Task<IActionResult> UpdateDishAsync(MenuDto menuDto)
        {
            try
            {
                // Uppdaterar rätten
                await _menuService.UpdateMenuAsync(menuDto);
                // Returnerar statuskod efter uppdatering
                return Ok();
            }
            catch (ArgumentException)
            {
                // Returnerar statuskod 404 (Not Found) om rätten inte hittas.
                return NotFound();
            }
            catch (Exception ex)
            {
                // Returnerar ett felmeddelande med statuskod 400 (Bad Request) vid andra undantag.
                return BadRequest(ex.Message);
            }
        }

        // DELETE-metod för att ta bort en rätt baserat på ID.
        [HttpDelete("/DeleteDish/{menuId}")]
        public async Task<IActionResult> DeleteDishAsync(int menuId)
        {
            try
            {
                // Tar bort en rätt
                await _menuService.DeleteDishAsync(menuId);
                // Returnerar statuskod efter lyckad borttagning.
                return Ok();
            }
            catch (ArgumentException)
            {
                // Returnerar statuskod 404 (Not Found) om rätten inte hittas.
                return NotFound();
            }
        }

    }
}
