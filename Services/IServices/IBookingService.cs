using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;

namespace Labb1_ResturantBookingSystem.Services.IServices
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto> GetBookingByIdAsync(int id);
        Task<BookingDto> CreateBookingAsync(BookingDto bookingDto);
        Task<BookingDto> UpdateBookingAsync(int id, BookingDto bookingDto);
        Task DeleteBookingAsync(int id);
    }
}
