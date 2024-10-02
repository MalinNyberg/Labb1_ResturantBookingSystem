using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;
using Labb1_ResturantBookingSystem.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Labb1_ResturantBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Dependency injection
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET-metod för att hämta alla kunder.
        [HttpGet("/GetAllCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersAsync()
        {
            // Hämtar alla kunder
            var customers = await _customerService.GetAllCustomersAsync();
            // Returnerar kunderna med statuskod 200 (OK).
            return Ok(customers);
        }

        //GET-metod för att hämta en specifik kund baserat på ID.
        [HttpGet("/GetCustomer/{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerAsync(int id)
        {
            // Hämtar en kund baserat på angivet ID.
            var customer = await _customerService.GetCustomerByIdAsync(id);
            // Om kunden inte finns, returnera statuskod 404 (Not Found).
            if (customer == null)
                return NotFound();

            // Returnerar kunden med statuskod 200 (OK).
            return Ok(customer);
        }

        // POST-metod för att skapa en ny kund.
        [HttpPost("/CreateCustomer")]
        public async Task<ActionResult<CustomerDto>> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            try
            {
                // Skapar en ny kund 
                await _customerService.CreateCustomerAsync(createCustomerDto);

                // Hämtar den skapade kunden baserat på det unika ID:t (justera vid behov).
                var createdCustomer = await _customerService.GetCustomerByIdAsync(createCustomerDto.Id);
                // Returnerar den skapade kunden med statuskod 201 (Created)
                return CreatedAtAction(nameof(GetCustomerAsync), new { id = createdCustomer.Id }, createdCustomer);
            }
            catch (Exception ex)
            {
                // Returnerar ett felmeddelande vid undantag.
                return BadRequest(ex.Message);
            }
        }

        // PUT-metod för att uppdatera en befintlig kund.
        [HttpPut("/UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomerAsync(int id, CustomerDto updateCustomerDto)
        {
            try
            {
                // Uppdaterar kunden
                await _customerService.UpdateCustomerAsync(id, updateCustomerDto);
                // Returnerar statuskod efter uppdatering.
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                // Returnerar statuskod 404 (Not Found) om kunden inte hittas.
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Returnerar ett felmeddelande vid andra undantag.
                return BadRequest(ex.Message);
            }
        }

        // DELETE-metod för att ta bort en kund baserat på ID.
        [HttpDelete("/DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            try
            {
                // Tar bort en kund 
                await _customerService.DeleteCustomerAsync(id);
                // Returnerar efter lyckad borttagning.
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                // Returnerar statuskod om kunden inte hittas.
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Returnerar ett felmeddelande vid undantag.
                return BadRequest(ex.Message);
            }
        }
    }  
}
