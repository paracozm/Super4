using Super4.Application.DataContract.Request.Product;
using Super4.Application.DataContract.Response.Product;
using Super4.Domain.Model;

namespace Super4.Application.Interfaces
{
    public interface IProductApplication
    {
        Task<Product> CreateAsync(CreateProductRequest product);
        Task<Product> UpdateAsync(string id, UpdateProductRequest product);
        Task<List<ProductResponse>> GetAllAsync();
        Task<ProductResponse> GetByIdAsync(string productId);
    }
}
