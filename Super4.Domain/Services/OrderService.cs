using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Services;
using Super4.Domain.Model;
using Super4.Domain.Validations;

namespace Super4.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerService _customerService;

        public OrderService(IUnitOfWork unitOfWork, ICustomerService customerService)
        {
            _unitOfWork = unitOfWork;
            _customerService = customerService;
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

            var getId = await _unitOfWork.CustomerRepository.GetIdByDocumentAsync(order.Customer.Document.Replace("-", "").Replace(".", ""));

            if (getId == null) 
            {
                await _customerService.CreateAsync(order.Customer);
                var newCustomer = await _unitOfWork.CustomerRepository.GetIdByDocumentAsync(order.Customer.Document);
                order.Customer.Id = newCustomer.Id;
            }else{
                var existingCustomer = await _unitOfWork.CustomerRepository.GetIdByDocumentAsync(order.Customer.Document.Replace("-", "").Replace(".", ""));
                order.Customer.Id = existingCustomer.Id;
            }

            order.CustomerId = order.Customer.Id;

            /*var customerExist = await _unitOfWork.CustomerRepository.ExistsById(order.Customer.Id);
            if (!customerExist)
            {
                throw new ArgumentException($"ERROR: Customer {order.Customer.Id} doesn't exist");
            }*/

            _unitOfWork.BeginTransaction();
            try
            {
                order.OrderNumber = Guid.NewGuid().ToString("N");
                order.Id = Guid.NewGuid().ToString("N");
                order.OrderDate = DateTime.UtcNow;

                foreach (var item in order.Items)
                {
                    if (item.ProductPrice <= 0)
                    {
                        throw new ArgumentException("ERROR: Product price must be a positive value.");
                    }
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
                    
                    if (stock == null)
                    {
                        throw new ArgumentException($"ERROR: Stock {item.Product.Id} is 0");
                    }
                    if (stock.Quantity < 0)
                    {
                        throw new ArgumentException($"ERROR: Stock {item.Product.Id} is not enough, current stock is: {stock.Quantity += item.TotalAmount}");
                    }
                    
                    stock.Quantity -= item.TotalAmount;

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
