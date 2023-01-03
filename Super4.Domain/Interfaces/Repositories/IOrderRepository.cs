using Super4.Domain.Model;

namespace Super4.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task CreateItemAsync(OrderItem item);
        Task<Order> GetByIdAsync(string orderId);
        Task<List<Order>> GetAllAsync();
        Task<List<OrderItem>> GetItemByOrderIdAsync(string orderId);
        Task<bool> ExistsById(string orderId);
        
    }
}
