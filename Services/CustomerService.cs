using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;

namespace Labb1_ResturantBookingSystem.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _customerRepository.AddCustomerAsync(customer);
            await _customerRepository.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(int id, Customer updatedCustomer)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
                throw new ArgumentException("Customer not found.");

            customer.Name = updatedCustomer.Name;            
            customer.PhoneNumber = updatedCustomer.PhoneNumber;

            await _customerRepository.UpdateCustomerAsync(customer);
            await _customerRepository.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
            await _customerRepository.SaveChangesAsync();
        }
    }
}
