using Super4.Domain.Model;

namespace Super4.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(Order order);
        Task<Order> GetByIdAsync(string orderId);
        Task<List<Order>> GetAllAsync();
    }
}
