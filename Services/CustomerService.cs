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

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber
            });
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public async Task CreateCustomerAsync(CreateCustomerDto createDto)
        {
            var customer = new Customer
            {
                Name = createDto.Name,
                PhoneNumber = createDto.PhoneNumber
            };

            await _customerRepository.AddCustomerAsync(customer);
            await _customerRepository.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(int id, CustomerDto updatedDto)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null) throw new ArgumentException("Customer not found.");

            customer.Name = updatedDto.Name;
            customer.PhoneNumber = updatedDto.PhoneNumber;

            await _customerRepository.UpdateCustomerAsync(customer);
            await _customerRepository.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null) throw new ArgumentException("Customer not found.");

            await _customerRepository.DeleteCustomerAsync(id);
            await _customerRepository.SaveChangesAsync();
        }
    }
}
