using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;
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

        // GET-metod för att hämta alla bord.
        [HttpGet("/GetTables")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetTablesAsync()
        {
            // Hämtar alla bord
            var tables = await _tableService.GetAllTablesAsync();
            // Returnerar borden med statuskod 200 (OK).
            return Ok(tables);
        }

        // GET-metod för att hämta ett specifikt bord baserat på ID.
        [HttpGet("/GetTable/{id}")]
        public async Task<ActionResult<TableDto>> GetTableAsync(int id)
        {
            // Hämtar ett bord baserat på angivet ID.
            var table = await _tableService.GetTableByIdAsync(id);
            // Om bordet inte finns, returnera statuskod 404 (Not Found).
            if (table == null)
            {
                return NotFound();
            }
            // Returnerar bordet med statuskod 200 (OK).
            return Ok(table);
        }

        // POST-metod för att skapa ett nytt bord.
        [HttpPost("/CreateTable")]
        public async Task<ActionResult<TableDto>> CreateTableAsync(CreateTableDto createTableDto)
        {
            try
            {
                // Skapar ett nytt bord 
                await _tableService.CreateTableAsync(createTableDto);
                // Hämtar det skapade bordet baserat på bordets nummer (justera vid behov).
                var createdTable = await _tableService.GetTableByIdAsync(createTableDto.TableNumber);
                // Returnerar det skapade bordet med statuskod 201 (Created)
                return CreatedAtAction(nameof(GetTableAsync), new { id = createdTable.TableId }, createdTable);
            }
            catch (Exception ex)
            {
                // Returnerar ett felmeddelande vid undantag.
                return BadRequest(ex.Message);
            }
        }

        // PUT-metod för att uppdatera ett befintligt bord.
        [HttpPut("/UpdateTable/{id}")]
        public async Task<IActionResult> UpdateTableAsync(int id, CreateTableDto updateTableDto)
        {
            // Kontrollerar att det angivna ID:t stämmer överens med bordets nummer i uppdateringsobjektet.
            if (id != updateTableDto.TableNumber)
            {
                // Returnerar statuskod 400 (Bad Request) om ID:t inte stämmer.
                return BadRequest();
            }

            try
            {
                // Uppdaterar bordet
                await _tableService.UpdateTableAsync(id, updateTableDto);
                // Returnerar statuskod efter lyckad uppdatering.
                return NoContent();
            }
            catch (ArgumentException)
            {
                // Returnerar statuskod 404 (Not Found) om bordet inte hittas.
                return NotFound();
            }
        }

        // DELETE-metod för att ta bort ett bord baserat på ID.
        [HttpDelete("/DeleteTable/{id}")]
        public async Task<IActionResult> DeleteTableAsync(int id)
        {
            try
            {
                // Tar bort ett bord 
                await _tableService.DeleteTableAsync(id);
                // Returnerar statuskod efter lyckad borttagning.
                return NoContent();
            }
            catch (ArgumentException)
            {
                // Returnerar statuskod 404 (Not Found) om bordet inte hittas.
                return NotFound();
            }
        }

    }
}
