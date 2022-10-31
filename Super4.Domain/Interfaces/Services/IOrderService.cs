using Super4.Domain.Model;

namespace Super4.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task CreateAsync(Order order);
        Task CreateItemAsync(OrderItem item);
        Task<Order> GetByIdAsync(string orderId);
        Task<List<Order>> GetAllAsync();
        Task<List<OrderItem>> GetItemByIdAsync(string orderId);
    }
}
