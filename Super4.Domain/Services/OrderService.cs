using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Services;
using Super4.Domain.Model;
using Super4.Domain.Validations;
using System.Runtime.InteropServices;

namespace Super4.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            var validation = new OrderValidation();
            var result = validation.Validate(order);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    throw new ArgumentException("Property: " + error.PropertyName + " failed validation. Error was: " + error.ErrorMessage);
                }
                return new Order();
            }

            var customerExist = await _unitOfWork.CustomerRepository.ExistsById(order.Customer.Id);
            if (!customerExist)
            {
                throw new ArgumentException($"ERROR: Customer {order.Customer.Id} doesn't exist");
            }

           

            //certo ou dentro do try-catch?

            _unitOfWork.BeginTransaction();
            try
            {
                order.OrderNumber = Guid.NewGuid().ToString("N");
                order.Id = Guid.NewGuid().ToString("N");
                order.OrderDate = DateTime.UtcNow;

                foreach (var item in order.Items)
                {
                    if (item.TotalAmount <= 0)
                    {
                        throw new ArgumentException("ERROR: Total amount must be a positive value.");
                    }

                    order.TotalPrice = item.ProductPrice * item.TotalAmount;

                    var productExist = await _unitOfWork.ProductRepository.ExistsById(item.Product.Id);
                    if (!productExist)
                    {
                        throw new ArgumentException($"ERROR: Product {item.Product.Id} doesn't exist");
                    }

                    var stock = await _unitOfWork.StockRepository.GetByIdAsync(item.Product.Id);
                    
                    stock.Quantity -= item.TotalAmount;
                    if (stock.Quantity < 0)
                    {
                        throw new ArgumentException($"ERROR: Stock {item.Product.Id} is not enough, current stock is: {stock.Quantity += item.TotalAmount}");
                    }
                    if (stock == null)
                    {
                        throw new ArgumentException($"ERROR: Stock {item.Product.Id} is 0");
                    }
                    await _unitOfWork.StockRepository.UpdateAsync(stock); 
                    
                }
                

                await _unitOfWork.OrderRepository.CreateAsync(order);
                _unitOfWork.CommitTransaction();
                return new Order();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Order>> GetAllAsync()
        {
            var response = await _unitOfWork.OrderRepository.GetAllAsync();
            
            return new List<Order>(response);
        }

        public async Task<Order> GetByIdAsync(string orderId)
        {
            var response = new Order();
            var data = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);


            data.Items = await _unitOfWork.OrderRepository.GetItemByOrderIdAsync(orderId);

            response = data;
            return response;
        }

        
    }
}
