using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;
using Labb1_ResturantBookingSystem.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb1_ResturantBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        // Depency injection
        private readonly IBookingService _bookingService;
      
        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET-metod för att hämta alla bokningar.
        [HttpGet("/GetAllBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsAsync()
        {
            // Hämtar alla bokningar via bokningstjänsten.
            var bookings = await _bookingService.GetAllBookingsAsync();
            // Returnerar bokningarna med statuskod 200 (OK).
            return Ok(bookings);
        }

        // GET-metod för att hämta en specifik bokning baserat på ID.
        [HttpGet("/GetBooking/{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingAsync(int id)
        {
            // Hämtar en bokning baserat på angivet ID.
            var booking = await _bookingService.GetBookingByIdAsync(id);
            // Om bokningen inte finns, returnerar statuskod 404 (Not Found).
            if (booking == null)
                return NotFound();

            // Returnerar bokningen med statuskod 200 (OK).
            return Ok(booking);
        }

        // GET-metod för att kontrollera tillgänglighet av ett bord på ett visst datum.
        [HttpGet("/availability")]
        public async Task<ActionResult<bool>> IsTableAvailable(int tableId, DateTime date)
        {
            try
            {
                // Kontrollerar om ett bord är tillgängligt på ett givet datum.
                var isAvailable = await _bookingService.IsTableAvailableAsync(tableId, date);
                // Returnerar resultatet (sant eller falskt) med statuskod 200 (OK).
                return Ok(isAvailable);
            }
            catch (Exception ex)
            {
                // Returnerar ett felmeddelande med statuskod 400 (Bad Request) vid undantag.
                return BadRequest(ex.Message);
            }
        }

        //POST-metod för att skapa en ny bokning.
        [HttpPost("/CreateBooking")]
        public async Task<ActionResult<BookingDto>> CreateBookingAsync(BookingDto bookingDto)
        {
            try
            {
                // Skapar en ny bokning med hjälp av tjänsten.
                await _bookingService.CreateBookingAsync(bookingDto);
                // Hämtar den skapade bokningen baserat på CustomerId.
                var createdBooking = await _bookingService.GetBookingByIdAsync(bookingDto.Id);
                // Returnerar den skapade bokningen med statuskod 201 (Created) och en referens till metoden GetBookingAsync.
                return CreatedAtAction(nameof(GetBookingAsync), new { id = createdBooking.Id }, createdBooking);
            }
            catch (Exception ex)
            {
                // Returnerar ett felmeddelande med statuskod 400 (Bad Request) vid undantag.
                return BadRequest(ex.Message);
            }
        }

        //PUT-metod för att uppdatera en befintlig bokning.
        [HttpPut("/UpdateBooking/{id}")]
        public async Task<IActionResult> UpdateBookingAsync(int id, BookingDto updateBookingDto)
        {
            // Kontrollera om ID:t i URL:en matchar ID:t i DTO:n.
            if (id != updateBookingDto.Id)
            {
                // Returnerar statuskod 400 (Bad Request) om ID:na inte matchar.
                return BadRequest();
            }

            try
            {
                // Uppdaterar bokningen via bokningstjänsten.
                await _bookingService.UpdateBookingAsync(id, updateBookingDto);
                // Returnerar statuskod 204 (No Content) om uppdateringen lyckats.
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                // Returnerar statuskod 404 (Not Found) om bokningen inte hittas.
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Returnerar ett felmeddelande med statuskod 400 (Bad Request) vid andra undantag.
                return BadRequest(ex.Message);
            }
        }

        // DELETE-metod för att ta bort en bokning baserat på ID.
        [HttpDelete("/DeleteBooking/{id}")]
        public async Task<IActionResult> DeleteBookingAsync(int id)
        {
            try
            {
                // Tar bort en bokning via bokningstjänsten.
                await _bookingService.DeleteBookingAsync(id);
                // Returnerar statuskod 204 (No Content) efter lyckad borttagning.
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                // Returnerar statuskod 404 (Not Found) om bokningen inte hittas.
                return NotFound(ex.Message);
            }
        }
    }
}
