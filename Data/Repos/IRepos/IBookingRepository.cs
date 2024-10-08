using Labb1_ResturantBookingSystem.Models;

namespace Labb1_ResturantBookingSystem.Data.Repos.IRepos
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        //Task<Booking> GetBookingByCustomerNameAsync(string customerName);
        Task<Booking> GetBookingByIdAsync(int id);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int id);
        Task SaveChangesAsync();
    }
}
