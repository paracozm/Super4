using AutoMapper;
using Super4.Application.DataContract.Request.Customer;
using Super4.Application.DataContract.Response.Customer;
using Super4.Application.Interfaces;
using Super4.Domain.Interfaces.Services;
using Super4.Domain.Model;

namespace Super4.Application.Application
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomerApplication(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public async Task<Customer> CreateAsync(CreateCustomerRequest customer)
        {
            var customerModel = _mapper.Map<Customer>(customer);
            return await _customerService.CreateAsync(customerModel);
        }

        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            List<Customer> result = await _customerService.GetAllAsync();
            var response = _mapper.Map<List<CustomerResponse>>(result);
            return response;
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var result = await _customerService.GetByIdAsync(id);
            var response = _mapper.Map<CustomerResponse>(result);
            return response;
        }
    }
}
