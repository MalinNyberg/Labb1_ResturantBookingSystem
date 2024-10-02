using Labb1_ResturantBookingSystem.Data.Repos.IRepos;
using Labb1_ResturantBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1_ResturantBookingSystem.Data.Repos
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MomokoRestuarantDbContext _context;

        public CustomerRepository(MomokoRestuarantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.customers.ToListAsync();    
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.customers.FindAsync(id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.customers.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _context.customers.FindAsync(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.PhoneNumber = customer.PhoneNumber;

                _context.customers.Update(existingCustomer); 
            }
            else
            {
                throw new ArgumentException("Customer not found.");
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.customers.FindAsync(id);
            if (customer != null)
            {
                _context.customers.Remove(customer);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
