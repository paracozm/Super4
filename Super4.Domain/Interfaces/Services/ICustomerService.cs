using Super4.Domain.Model;

namespace Super4.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer> GetByIdAsync(int customerId);
        Task<List<Customer>> GetAllAsync();
    }
}