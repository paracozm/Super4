using AutoMapper;
using Super4.Application.DataContract.Request.Order;
using Super4.Application.DataContract.Response.Order;
using Super4.Application.Interfaces;
using Super4.Domain.Interfaces.Services;
using Super4.Domain.Model;

namespace Super4.Application.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderApplication(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<Order> CreateAsync(FillOrder order)
        {
            try
            {
                var orderModel = _mapper.Map<Order>(order);
                return await _orderService.CreateAsync(orderModel);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<OrderResponse>> GetAllAsync()
        {
            List<Order> result = await _orderService.GetAllAsync();
            var response = _mapper.Map<List<OrderResponse>>(result);
            return response;
        }

        public async Task<OrderResponse> GetByIdAsync(string id)
        {
            var result = await _orderService.GetByIdAsync(id);
            var response = _mapper.Map<OrderResponse>(result);
            return response;
        }
    }
}
