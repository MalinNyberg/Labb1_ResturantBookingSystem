using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Services.IServices;

namespace Labb1_ResturantBookingSystem.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITableRepository _tableRepository;

        public BookingService(IBookingRepository bookingRepository, ITableRepository tableRepository)
        {
            _bookingRepository = bookingRepository;
            _tableRepository = tableRepository;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllBookingsAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _bookingRepository.GetBookingByIdAsync(id);
        }

        public async Task<bool> IsTableAvailableAsync(int tableId, DateTime date)
        {
            var existingBookings = await _bookingRepository.GetAllBookingsAsync();
            return !existingBookings.Any(b => b.TableId == tableId && b.Date == date);
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            if (!await IsTableAvailableAsync(booking.TableId, booking.Date))
                throw new Exception("Table is not available for the selected date and time.");

            await _bookingRepository.AddBookingAsync(booking);
            await _bookingRepository.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(int id, Booking updatedBooking)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null)
                throw new ArgumentException("Booking is not found.");

            booking.Date = updatedBooking.Date;
            booking.TableId = updatedBooking.TableId;
            booking.Customer = updatedBooking.Customer;

            await _bookingRepository.UpdateBookingAsync(booking);
            await _bookingRepository.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int id)
        {
            await _bookingRepository.DeleteBookingAsync(id);
            await _bookingRepository.SaveChangesAsync();
        }
    }
}
