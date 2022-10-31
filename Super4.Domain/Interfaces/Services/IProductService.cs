using Super4.Domain.Model;

namespace Super4.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task<Product> GetByIdAsync(string productId);
        Task<List<Product>> GetAllAsync();
    }
}
