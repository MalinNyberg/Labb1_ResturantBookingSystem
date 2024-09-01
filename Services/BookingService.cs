using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;
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

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();

            // Convert Booking entities to BookingDto
            return bookings.Select(b => new BookingDto
            {
                Id = b.BookingId,
                CustomerId = b.CustomerId,
                TableId = b.TableId,
                Date = b.Date,
                Time = b.Time
            });
        }

        public async Task<BookingDto> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null) return null;

            // Convert Booking entity to BookingDto
            return new BookingDto
            {
                Id = booking.BookingId,
                CustomerId = booking.CustomerId,
                TableId = booking.TableId,
                Date = booking.Date,
                Time = booking.Time
            };
        }

        public async Task<bool> IsTableAvailableAsync(int tableId, DateTime date)
        {
            var existingBookings = await _bookingRepository.GetAllBookingsAsync();
            return !existingBookings.Any(b => b.TableId == tableId && b.Date.Date == date.Date && b.Time == date.TimeOfDay);
        }

        public async Task CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            if (!await IsTableAvailableAsync(createBookingDto.TableId, createBookingDto.Date))
                throw new Exception("Table is not available for the selected date and time.");

            var booking = new Booking
            {
                CustomerId = createBookingDto.CustomerId,
                TableId = createBookingDto.TableId,
                Date = createBookingDto.Date,
                Time = createBookingDto.Time
            };

            await _bookingRepository.AddBookingAsync(booking);
            await _bookingRepository.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(int id, CreateBookingDto updateBookingDto)
        {
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(id);
            if (existingBooking == null)
            {
                throw new ArgumentException("Booking not found.");
            }

            existingBooking.CustomerId = updateBookingDto.CustomerId;
            existingBooking.TableId = updateBookingDto.TableId;
            existingBooking.Date = updateBookingDto.Date;
            existingBooking.Time = updateBookingDto.Time;

            await _bookingRepository.UpdateBookingAsync(existingBooking);
            await _bookingRepository.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int id)
        {
            await _bookingRepository.DeleteBookingAsync(id);
            await _bookingRepository.SaveChangesAsync();
        }
    }
}
