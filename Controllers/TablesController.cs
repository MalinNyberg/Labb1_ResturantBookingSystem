using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb1_ResturantBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;
        public TablesController(ITableService tableService) 
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetTablesAsync() 
        {
            var Tables = await _tableService.GetAllTablesAsync();
            return Ok(Tables);
        
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTableAsync(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return Ok(table);
        }

        [HttpPost]
        public async Task<ActionResult<Table>> CreateTableAsync(Table table)
        {
            await _tableService.CreateTableAsync(table);
            return CreatedAtAction(nameof(GetTableAsync),new {id = table.TableId}, table);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTableAsync(int id, Table table)
        {
            if (id != table.TableId)
            {
                return BadRequest();
            }

            try
            {
                await _tableService.UpdateTableAsync(id, table);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTableAsync(int id)
        {
            try
            {
                await _tableService.DeleteTableAsync(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
