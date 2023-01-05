using Super4.Domain.Model;

namespace Super4.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer customer);
        Task<Customer> GetByIdAsync(int customerId);
        Task<List<Customer>> GetAllAsync();
        Task<bool> ExistsById(int customerId);
        Task<int> GetLastId();
    }
}
