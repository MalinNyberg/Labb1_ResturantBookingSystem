using Labb1_ResturantBookingSystem.Data.Repos;
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
        private readonly ICustomerRepository _customerRepository;
        public BookingService(IBookingRepository bookingRepository, ITableRepository tableRepository, ICustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _tableRepository = tableRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();
            return bookings.Select(b => new BookingDto
            {
                Id = b.BookingId,
                CustomerName = b.CustomerName,
                PhoneNumber = b.PhoneNumber,
                Email = b.Email,
                NumberOfPeople = b.NumberOfPeople,
                Date = b.Date
            });
        }

        public async Task<BookingDto> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null) return null;

            return new BookingDto
            {
                Id = booking.BookingId,
                CustomerName = booking.CustomerName,
                PhoneNumber = booking.PhoneNumber,
                Email = booking.Email,
                NumberOfPeople = booking.NumberOfPeople,
                Date = booking.Date
            };
        }

        public async Task<BookingDto> CreateBookingAsync(BookingDto bookingDto)
        {

            var booking = new Booking
            {
                CustomerName = bookingDto.CustomerName,
                PhoneNumber = bookingDto.PhoneNumber,
                Email = bookingDto.Email,
                NumberOfPeople = bookingDto.NumberOfPeople,
                Date = bookingDto.Date
            };

            await _bookingRepository.AddBookingAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            bookingDto.Id = booking.BookingId; 
            return bookingDto;
        }

        public async Task<BookingDto> UpdateBookingAsync(int id, BookingDto bookingDto)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null) return null;

            booking.BookingId = bookingDto.Id;
            booking.CustomerName = bookingDto.CustomerName;
            booking.PhoneNumber = bookingDto.PhoneNumber;
            booking.Email = bookingDto.Email;
            booking.NumberOfPeople = bookingDto.NumberOfPeople;
            booking.Date = bookingDto.Date;

            await _bookingRepository.UpdateBookingAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            return bookingDto;
        }

        public async Task DeleteBookingAsync(int id)
        {
            await _bookingRepository.DeleteBookingAsync(id);
            await _bookingRepository.SaveChangesAsync();
        }
    }
}
