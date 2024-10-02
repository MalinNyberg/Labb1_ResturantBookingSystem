using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;

namespace Labb1_ResturantBookingSystem.Services.IServices
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto> GetBookingByIdAsync(int id);
        Task<bool> IsTableAvailableAsync(int tableId, DateTime date);
        Task CreateBookingAsync(BookingDto createBookingDto);
        Task UpdateBookingAsync(int id, BookingDto createBookingDto);
        Task DeleteBookingAsync(int id);
    }
}
