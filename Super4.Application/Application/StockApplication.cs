using AutoMapper;
using Super4.Application.DataContract.Request.Product;
using Super4.Application.DataContract.Request.Stock;
using Super4.Application.DataContract.Response.Product;
using Super4.Application.DataContract.Response.Stock;
using Super4.Application.Interfaces;
using Super4.Domain.Interfaces.Services;
using Super4.Domain.Model;
using Super4.Domain.Services;

namespace Super4.Application.Application
{
    public class StockApplication : IStockApplication
    {
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public StockApplication(IStockService stockService, IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }

        public async Task<Stock> CreateAsync(CreateStockRequest stock)
        {
            var stockModel = _mapper.Map<Stock>(stock);
            return await _stockService.CreateAsync(stockModel);
        }

        public async Task<List<StockResponse>> GetAllAsync()
        {
            List<Stock> result = await _stockService.GetAllAsync();
            var response = _mapper.Map<List<StockResponse>>(result);
            return response;
        }

        public async Task<StockResponse> GetByIdAsync(string productId)
        {
            var result = await _stockService.GetByIdAsync(productId);
            var response = _mapper.Map<StockResponse>(result);
            return response;
        }

        public async Task<Stock> UpdateAsync(string id, UpdateStockRequest stock)
        {
            stock.ProductId = id;
            var stockModel = _mapper.Map<Stock>(stock);
            return await _stockService.UpdateAsync(stockModel);
        }
    }
}
