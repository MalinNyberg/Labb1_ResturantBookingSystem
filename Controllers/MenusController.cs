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
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("/GetAllDishes")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetAllDishesAsync()
        {
            var dishes = await _menuService.GetAllDishesAsync();
            return Ok(dishes);
        }

        [HttpGet("/GetDish/{id}")]
        public async Task<ActionResult<MenuDto>> GetDishByIdAsync(int id)
        {
            var dish = await _menuService.GetDishByIdAsync(id);

            if (dish == null)
            {
                return NotFound();
            }
            return Ok(dish);
        }

        [HttpGet("/IsAvailable/{id}")]
        public async Task<ActionResult<bool>> IsDishAvailableAsync(int id)
        {
            var isAvailable = await _menuService.IsDishAvailableAsync(id);
            return Ok(isAvailable);
        }

        [HttpPost("/AddDish")]
        public async Task<ActionResult<MenuDto>> AddDishAsync(int id, CreateMenuDto createMenuDto)
        {
            try
            {
                await _menuService.AddDishAsync(createMenuDto);

                // Assuming menu ID is set during creation (from the database)
                // i might need to fetch the newly created menu item???
                var createdDish = await _menuService.GetDishByIdAsync(id);
                if (createdDish == null)
                {
                    return NotFound();
                }
                return CreatedAtAction(nameof(GetDishByIdAsync), new { id = createdDish.MenuId }, createdDish);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/UpdateDish{id}")]
        public async Task<IActionResult> UpdateMenuAsync(int id, UpdateMenuDto updateMenuDto)
        {
           
            try
            {
                await _menuService.UpdateMenuAsync(id, updateMenuDto);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/DeleteDish{id}")]
        public async Task<IActionResult> DeleteDishAsync(int id)
        {
            try
            {
                await _menuService.DeleteDishAsync(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

    }
}
