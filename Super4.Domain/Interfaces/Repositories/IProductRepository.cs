using Super4.Domain.Model;

namespace Super4.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task<Product> GetByIdAsync(string productId);
        Task<List<Product>> GetAllAsync();
        Task<bool> ExistsById(string productId);
    }
}
