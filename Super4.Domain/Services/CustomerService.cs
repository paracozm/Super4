using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Services;
using Super4.Domain.Model;
using Super4.Domain.Validations;

namespace Super4.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            var validation = new CustomerValidation();
            var result = validation.Validate(customer);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    throw new ArgumentException("Property: " + error.PropertyName + " failed validation. Error was: " + error.ErrorMessage);
                }
                return new Customer();
            }

            var exists = await _unitOfWork.CustomerRepository.CPFExists(customer.Document.Replace(".", "").Replace("-", ""));
            if (exists)
            {
                throw new ArgumentException($"CPF: {customer.Document.Replace(".", "").Replace("-", "")} is already registered!");
            }

            await ViaCepService.GetCepInfo(customer);
            await CPFValidationService.CPFCheck(customer);

            await _unitOfWork.CustomerRepository.CreateAsync(customer);
            return new Customer();
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            var response = await _unitOfWork.CustomerRepository.GetAllAsync();
            return new List<Customer>(response);
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            var exists = await _unitOfWork.CustomerRepository.ExistsById(customerId);
            if (!exists)
            {
                throw new Exception($"Error: Customer {customerId} does not exist.");
            }
            
            var response = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            return response;
        }
    }
}
