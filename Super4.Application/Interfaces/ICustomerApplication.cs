using Super4.Application.DataContract.Request.Customer;
using Super4.Application.DataContract.Response.Customer;
using Super4.Domain.Model;

namespace Super4.Application.Interfaces
{
    public interface ICustomerApplication
    {
        Task<Customer> CreateAsync(CreateCustomerRequest customer);
        Task<List<CustomerResponse>> GetAllAsync();
        Task<CustomerResponse> GetByIdAsync(int customerId);
    }
}
