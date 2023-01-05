using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Services;
using Super4.Domain.Model;
using Super4.Domain.Validations;

namespace Super4.Domain.Services
{
    public class StockService : IStockService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            var validation = new StockValidation();

            var result = validation.Validate(stock);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    throw new ArgumentException("Property: " + error.PropertyName + " failed validation. Error was: " + error.ErrorMessage);
                }
                return new Stock();
            }

            var exists = await _unitOfWork.ProductRepository.ExistsById(stock.Product.Id);
            if (!exists)
            {
                throw new Exception($"Error: Product Id {stock.Product.Id} does not exist.");
            }

            await _unitOfWork.StockRepository.CreateAsync(stock);
            return new Stock();

        }
        public async Task<Stock> UpdateAsync(Stock stock)
        {
            var validation = new StockValidation();

            var result = validation.Validate(stock);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    throw new ArgumentException("Property: " + error.PropertyName + " failed validation. Error was: " + error.ErrorMessage);
                }
                return new Stock();
            }

            var productExists = await _unitOfWork.ProductRepository.ExistsById(stock.Product.Id);
            if (!productExists)
            {
                throw new Exception($"Error: Product Id {stock.Product.Id} does not exist.");
            }

            var stockExists = await _unitOfWork.StockRepository.GetByIdAsync(stock.Product.Id);
            if (stockExists == null)
            {
                throw new Exception($"Error: Stock for Product Id: {stock.Product.Id} does not exist.");
            }

            await _unitOfWork.StockRepository.UpdateAsync(stock);
            return new Stock();
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            var response = await _unitOfWork.StockRepository.GetAllAsync();
            return new List<Stock>(response);
        }

        public async Task<Stock> GetByIdAsync(string productId)
        {
            var exists = await _unitOfWork.ProductRepository.ExistsById(productId);
            if (!exists)
            {
                throw new Exception($"Error: Product {productId} does not exist.");
            }

            var response = await _unitOfWork.StockRepository.GetByIdAsync(productId);
            return response;
        }


    }
}
