using Labb1_ResturantBookingSystem.Models;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetAllDishesAsync()
        {
            var dishes = await _menuService.GetAllDishesAsync();
            return Ok(dishes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetDishByIdAsync(int id)
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
            var IsAvailable = await _menuService.IsDishAvailableAsync(id);
            return Ok(IsAvailable);
        }


        [HttpPost]
        public async Task<ActionResult<Menu>> AddDishAsync(Menu menu)
        {
            await _menuService.AddDishAsync(menu);
            return CreatedAtAction(nameof(GetDishByIdAsync), new { id = menu.MenuId }, menu);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuAsync(int id, Menu menu)
        {
            if (id != menu.MenuId)
            {
                return BadRequest();
            }

            try
            {
                await _menuService.UpdateMenuAsync(id, menu);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDishAsync(int id)
        {
            try
            {
                await _menuService.DeleteDishAsync(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent(); 
        }

    }
}
