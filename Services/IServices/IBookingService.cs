using Labb1_ResturantBookingSystem.Models;

namespace Labb1_ResturantBookingSystem.Services.IServices
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task<bool> IsTableAvailableAsync(int tableId, DateTime date);
        Task CreateBookingAsync(Booking booking);
        Task UpdateBookingAsync(int id, Booking booking);
        Task DeleteBookingAsync(int id);
    }
}
