using Super4.Domain.Model;

namespace Super4.Domain.Interfaces.Services
{
    public interface IStockService
    {
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock> UpdateAsync(Stock stock);
        Task<Stock> GetByIdAsync(string productId);
        Task<List<Stock>> GetAllAsync();
    }
}
