using Super4.Domain.Model;

namespace Super4.Domain.Interfaces.Repositories
{
    public interface IStockRepository
    {
        Task CreateAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task<Stock> GetByIdAsync(string productId);
        Task<List<Stock>> GetAllAsync();
    }
}
