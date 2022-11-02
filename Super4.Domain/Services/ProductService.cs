using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Services;
using Super4.Domain.Model;
using Super4.Domain.Validations;

namespace Super4.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var validation = new ProductValidation();

            var result = validation.Validate(product);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    throw new ArgumentException("Property: " + error.PropertyName + " failed validation. Error was: " + error.ErrorMessage);
                }
                return new Product();
            }

            product.Id = Guid.NewGuid().ToString("N");

            await _unitOfWork.ProductRepository.CreateAsync(product);
            return new Product();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var validation = new ProductValidation();

            var result = validation.Validate(product);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    throw new ArgumentException("Property: " + error.PropertyName + " failed validation. Error was: " + error.ErrorMessage);
                }
                return new Product();
            }

            var exists = await _unitOfWork.ProductRepository.ExistsById(product.Id);
            if (!exists)
            {
                throw new Exception($"Error: SKU Id {product.Id} does not exist.");
            }

            await _unitOfWork.ProductRepository.UpdateAsync(product);
            return new Product();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var response = await _unitOfWork.ProductRepository.GetAllAsync();
            return new List<Product>(response);
        }

        public async Task<Product> GetByIdAsync(string productId)
        {
            var exists = await _unitOfWork.ProductRepository.ExistsById(productId);
            if (!exists)
            {
                throw new Exception($"Error: Product {productId} does not exist.");
            }

            var response = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            return response;
        }


    }
}
