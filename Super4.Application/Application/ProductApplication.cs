using AutoMapper;
using Super4.Application.DataContract.Request.Product;
using Super4.Application.DataContract.Response.Customer;
using Super4.Application.DataContract.Response.Product;
using Super4.Application.Interfaces;
using Super4.Domain.Interfaces.Services;
using Super4.Domain.Model;
using Super4.Domain.Services;

namespace Super4.Application.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductApplication(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<Product> CreateAsync(CreateProductRequest product)
        {
            var productModel = _mapper.Map<Product>(product);
            return await _productService.CreateAsync(productModel);
        }

        public async Task<Product> UpdateAsync(string id, UpdateProductRequest product)
        {
            await _productService.GetByIdAsync(id);
            product.Id = id;
            
            var productModel = _mapper.Map<Product>(product);
            return await _productService.UpdateAsync(productModel);
        }

        public async Task<List<ProductResponse>> GetAllAsync()
        {
            List<Product> result = await _productService.GetAllAsync();
            var response = _mapper.Map<List<ProductResponse>>(result);
            return response;
        }

        public async Task<ProductResponse> GetByIdAsync(string productId)
        {
            var result = await _productService.GetByIdAsync(productId);
            var response = _mapper.Map<ProductResponse>(result);
            return response;
        }

        
    }
}
