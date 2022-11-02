using Super4.Application.DataContract.Request.Stock;
using Super4.Application.DataContract.Response.Stock;
using Super4.Domain.Model;

namespace Super4.Application.Interfaces
{
    public interface IStockApplication
    {
        Task<Stock> CreateAsync(CreateStockRequest stock);
        Task<Stock> UpdateAsync(string id, UpdateStockRequest stock);
        Task<List<StockResponse>> GetAllAsync();
        Task<StockResponse> GetByIdAsync(string productId);
    }
}
