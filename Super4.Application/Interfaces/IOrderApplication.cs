using Super4.Application.DataContract.Request.Order;
using Super4.Application.DataContract.Response.Order;
using Super4.Domain.Model;

namespace Super4.Application.Interfaces
{
    public interface IOrderApplication
    {
        Task<Order> CreateAsync(CreateOrderRequest order);
        Task<List<OrderResponse>> GetAllAsync();
        Task<OrderResponse> GetByIdAsync(string id);
    }
}
