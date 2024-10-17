using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;
using Labb1_ResturantBookingSystem.Services;
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

        [HttpGet("/Booking/GetAllBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("/Booking/GetBookingById/{Id}")]
        public async Task<ActionResult<BookingDto>> GetBookingById(int Id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(Id); 
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost("/Booking/CreateBooking")]
        public async Task<ActionResult<BookingDto>> CreateBooking(BookingDto bookingDto)
        {
            try
            {
                
                await _bookingService.CreateBookingAsync(bookingDto);

                // Returnera den skapade bokningen med statuskod 201 (Created)
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/Booking/UpdateBooking/{id}")]
        public async Task<ActionResult<BookingDto>> UpdateBooking(int id, BookingDto bookingDto)
        {
            var updatedBooking = await _bookingService.UpdateBookingAsync(id, bookingDto);
            if (updatedBooking == null) return NotFound();
            return Ok(updatedBooking);
        }

        [HttpDelete("/Booking/DeleteBooking/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return NoContent();
        }

        [HttpGet("/Booking/IsTableAvailable")]
        public async Task<IActionResult> IsTableAvailable([FromQuery] int tableId, [FromQuery] DateTime date)
        {
            try
            {
                bool isAvailable = await _bookingService.IsTableAvailableAsync(tableId, date);
                if (isAvailable)
                {
                    return Ok("The table is available.");
                }
                else
                {
                    return NotFound("The table is unavailable");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ett fel uppstod: {ex.Message}");
            }
        }

        [HttpGet("/Booking/AvailableTables/{date}")]
        public async Task<ActionResult<IEnumerable<Table>>> AvailableTablesAtSpecificDate(DateTime date)
        {
            var listOfAvailableTables = await _bookingService.AvailableTablesAsync(date);
            return Ok(listOfAvailableTables);
        }
    }
}
