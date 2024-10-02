using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Labb1_ResturantBookingSystem.Models.DTOs;
using Labb1_ResturantBookingSystem.Services.IServices;

namespace Labb1_ResturantBookingSystem.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // Metod för att hämta alla kunder.
        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            // Hämtar alla kunder
            var customers = await _customerRepository.GetAllCustomersAsync();

            // Konverterar Customer-entiteter till CustomerDto-objekt och returnerar dessa.
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber
            });
        }

        // Metod för att hämta en specifik kund baserat på ID.
        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            // Hämtar kunden baserat på ID
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            // Returnerar null om kunden inte hittas.
            if (customer == null) return null;

            // Konverterar Customer-entiteten till en CustomerDto och returnerar den.
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber
            };
        }

        // Metod för att skapa en ny kund.
        public async Task CreateCustomerAsync(CreateCustomerDto createDto)
        {
            // Skapar en ny kund baserat på CreateCustomerDto.
            var customer = new Customer
            {
                Name = createDto.Name,
                PhoneNumber = createDto.PhoneNumber
            };

            // Lägger till den nya kunden och sparar ändringarna.
            await _customerRepository.AddCustomerAsync(customer);
            await _customerRepository.SaveChangesAsync();
        }

        // Metod för att uppdatera en befintlig kund.
        public async Task UpdateCustomerAsync(int id, CustomerDto updatedDto)
        {
            // Hämtar den befintliga kunden baserat på ID 
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            // undantag om kunden inte hittas.
            if (customer == null) throw new ArgumentException("Kunden hittades inte.");

            // Uppdaterar kundens information med den nya informationen.
            customer.Name = updatedDto.Name;
            customer.PhoneNumber = updatedDto.PhoneNumber;

            // Uppdaterar kunden och sparar ändringarna.
            await _customerRepository.UpdateCustomerAsync(customer);
            await _customerRepository.SaveChangesAsync();
        }

        // Metod för att ta bort en kund baserat på ID.
        public async Task DeleteCustomerAsync(int id)
        {
            // Hämtar kunden baserat på ID
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            // undantag om kunden inte hittas.
            if (customer == null) throw new ArgumentException("Kunden hittades inte.");

            // Tar bort kunden och sparar ändringarna.
            await _customerRepository.DeleteCustomerAsync(id);
            await _customerRepository.SaveChangesAsync();
        }
    }
}
