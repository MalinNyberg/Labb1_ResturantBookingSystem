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

        // Metod för att hämta alla bokningar.
        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            // Hämtar alla bokningar
            var bookings = await _bookingRepository.GetAllBookingsAsync();

            // Konverterar Booking-entiteter till BookingDto-objekt och returnerar dessa.
            return bookings.Select(b => new BookingDto
            {
                Id = b.BookingId,
                CustomerName = b.CustomerName,
                PhoneNumber = b.PhoneNumber,
                Email = b.Email,
                NumberOfPeople = b.NumberOfPeople,
                Date = b.Date,
                
            });
        }

        // Metod för att hämta en specifik bokning baserat på ID.
        public async Task<BookingDto> GetBookingByIdAsync(int id)
        {
            // Hämtar bokningen baserat på ID
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            // Returnerar null om bokningen inte hittas.
            if (booking == null) return null;

            // Konverterar Booking-entiteten till en BookingDto och returnerar den.
            return new BookingDto
            {
                Id = booking.BookingId,
                CustomerName = booking.CustomerName,
                PhoneNumber = booking.PhoneNumber,
                Email = booking.Email,
                NumberOfPeople = booking.NumberOfPeople,
                Date = booking.Date,
                
            };
        }

        // Metod för att kontrollera om ett bord är tillgängligt vid ett visst datum och tid.
        public async Task<bool> IsTableAvailableAsync(int tableId, DateTime date)
        {
            // Hämtar alla bokningar 
            var existingBookings = await _bookingRepository.GetAllBookingsAsync();
            // Returnerar true om bordet är tillgängligt (ingen annan bokning för samma datum och tid).
            return !existingBookings.Any(b => b.TableId == tableId && b.Date.Date == date.Date);
        }

        // Metod för att skapa en ny bokning.
        public async Task CreateBookingAsync(BookingDto createBookingDto)
        {
            // Kontrollera om bordet är tillgängligt för det valda datumet och tiden.
            if (!await IsTableAvailableAsync(createBookingDto.TableId, createBookingDto.Date))
                throw new Exception("Bordet är inte tillgängligt för det valda datumet och tiden.");

            // Skapar en ny bokning baserat på CreateBookingDto.
            var booking = new Booking
            {
                CustomerName = createBookingDto.CustomerName,
                TableId = createBookingDto.TableId,
                Date = createBookingDto.Date               
            };

            // Lägger till den nya bokningen och sparar ändringarna.
            await _bookingRepository.AddBookingAsync(booking);
            await _bookingRepository.SaveChangesAsync();
        }

        // Metod för att uppdatera en befintlig bokning.
        public async Task UpdateBookingAsync(int id, BookingDto updateBookingDto)
        {
            // Hämtar den befintliga bokningen baserat på ID
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(id);
            // Kastar ett undantag om bokningen inte hittas.
            if (existingBooking == null)
            {
                throw new ArgumentException("Bokning hittades inte.");
            }

            // Uppdaterar bokningens information med den nya informationen.
            existingBooking.CustomerName = updateBookingDto.CustomerName;
            existingBooking.PhoneNumber = updateBookingDto.PhoneNumber;
            existingBooking.Email = updateBookingDto.Email;
            existingBooking.TableId = updateBookingDto.TableId;
            existingBooking.Date = updateBookingDto.Date;

            // Uppdaterar bokningen  och sparar ändringarna.
            await _bookingRepository.UpdateBookingAsync(existingBooking);
            await _bookingRepository.SaveChangesAsync();
        }

        // Metod för att ta bort en bokning baserat på ID.
        public async Task DeleteBookingAsync(int id)
        {
            // Tar bort bokningen och sparar ändringarna.
            await _bookingRepository.DeleteBookingAsync(id);
            await _bookingRepository.SaveChangesAsync();
        }
    }
}
