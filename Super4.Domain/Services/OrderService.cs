using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Services;
using Super4.Domain.Model;
using Super4.Domain.Validations;

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
            
            _unitOfWork.BeginTransaction();
            try
            {

                /*var validation = new OrderValidation();

                var result = validation.Validate(order);
                if (!result.IsValid)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new ArgumentException("Property: " + error.PropertyName + " failed validation. Error was: " + error.ErrorMessage);
                    }
                    return new Order();
                }
                */


                order.OrderNumber = Guid.NewGuid().ToString("N");
                order.Id = Guid.NewGuid().ToString("N");
                order.OrderDate = DateTime.UtcNow;

                foreach (var item in order.Items)
                {
                    order.TotalPrice = item.ProductPrice * item.TotalAmount;
                }


                /*
                if (order.Stocks.Any())
                {
                    foreach (var stock in order.Stocks)
                    {
                        var sumStock = (stock.Quantity - stock.Item.TotalAmount);
                        await _unitOfWork.StockRepository.UpdateAsync(stock);
                    }
                }

                */
                await _unitOfWork.OrderRepository.CreateAsync(order);

                _unitOfWork.CommitTransaction();
                return new Order();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return new Order();
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
