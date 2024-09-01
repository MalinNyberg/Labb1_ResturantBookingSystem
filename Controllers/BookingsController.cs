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
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsAsync()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingAsync(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpGet("availability")]
        public async Task<ActionResult<bool>> IsTableAvailable(int tableId, DateTime date)
        {
            try
            {
                var isAvailable = await _bookingService.IsTableAvailableAsync(tableId, date);
                return Ok(isAvailable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            try
            {
                await _bookingService.CreateBookingAsync(createBookingDto);
                //the created booking ID is supposed to be retrievable here
                var createdBooking = await _bookingService.GetBookingByIdAsync(createBookingDto.CustomerId);
                return CreatedAtAction(nameof(GetBookingAsync), new { id = createdBooking.Id }, createdBooking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookingAsync(int id, CreateBookingDto updateBookingDto)
        {
            if (id != updateBookingDto.CustomerId)
            {
                return BadRequest();
            }

            try
            {
                await _bookingService.UpdateBookingAsync(id, updateBookingDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingAsync(int id)
        {
            try
            {
                await _bookingService.DeleteBookingAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
