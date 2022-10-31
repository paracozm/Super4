using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Services;
using Super4.Domain.Model;
using Super4.Domain.Validations;
using FluentValidation.Results;

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
            var response = new Customer();
            var validation = new CustomerValidation();

            var exists = await _unitOfWork.CustomerRepository.CPFExists(customer.Document.Replace(".", "").Replace("-", ""));
            if (exists)
            {
                throw new ArgumentException($"CPF: {customer.Document.Replace(".", "").Replace("-", "")} is already registered!");
            }

            var result = validation.Validate(customer);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    throw new ArgumentException("Property: " + error.PropertyName + " failed validation. Error was: " + error.ErrorMessage);
                }
                return response;
            }

            await ViaCepService.GetCepInfo(customer);
            await CPFValidationService.CPFCheck(customer);

            await _unitOfWork.CustomerRepository.CreateAsync(customer);
            return response;
        }

        public Task<List<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
