using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;
using Labb1_ResturantBookingSystem.Services;
using Labb1_ResturantBookingSystem.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Labb1_ResturantBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("/IsAvailable/{id}")]
        public async Task<ActionResult<bool>> IsDishAvailableAsync(int id)
        {
            // Kontrollerar om rätten är tillgänglig
            var isAvailable = await _menuService.IsDishAvailableAsync(id);
            // Returnerar resultatet (sant eller falskt) med statuskod 200 (OK).
            return Ok(isAvailable);
        }

        // POST-metod för att lägga till en ny rätt.
        [HttpPost("/AddDish")]
        public async Task<ActionResult<MenuDto>> AddDishAsync(int id, CreateMenuDto createMenuDto)
        {
            try
            {
                // Lägger till en ny rätt
                await _menuService.AddDishAsync(createMenuDto);

                // Hämtar den nyligen skapade rätten baserat på ID (justera vid behov).
                var createdDish = await _menuService.GetDishByIdAsync(id);
                // Om den skapade rätten inte hittas, returnera 404 (Not Found).
                if (createdDish == null)
                {
                    return NotFound();
                }
                // Returnerar den skapade rätten med statuskod 201 (Created)
                return CreatedAtAction(nameof(GetDishByIdAsync), new { id = createdDish.MenuId }, createdDish);
            }
            catch (Exception ex)
            {
                // Returnerar ett felmeddelande vid undantag.
                return BadRequest(ex.Message);
            }
        }

        // PUT-metod för att uppdatera en befintlig rätt.
        [HttpPut("/UpdateDish{id}")]
        public async Task<IActionResult> UpdateMenuAsync(int id, UpdateMenuDto updateMenuDto)
        {
            try
            {
                // Uppdaterar rätten
                await _menuService.UpdateMenuAsync(id, updateMenuDto);
                // Returnerar statuskod efter uppdatering
                return NoContent();
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
        [HttpDelete("/DeleteDish{id}")]
        public async Task<IActionResult> DeleteDishAsync(int id)
        {
            try
            {
                // Tar bort en rätt
                await _menuService.DeleteDishAsync(id);
                // Returnerar statuskod efter lyckad borttagning.
                return NoContent();
            }
            catch (ArgumentException)
            {
                // Returnerar statuskod 404 (Not Found) om rätten inte hittas.
                return NotFound();
            }
        }

    }
}
