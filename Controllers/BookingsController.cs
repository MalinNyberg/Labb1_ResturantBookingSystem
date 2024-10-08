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
                
                var createdBookingDto = await _bookingService.CreateBookingAsync(bookingDto);

                // Returnera den skapade bokningen med statuskod 201 (Created)
                return CreatedAtAction(nameof(GetBookingById), new {  id = createdBookingDto.Id }, createdBookingDto);
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
    }
}
