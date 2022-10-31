using DocumentValidator;
using Super4.Domain.Model;

namespace Super4.Domain.Validations
{
    public static class CPFValidationService
    {
        public static async Task<bool> CPFCheck(Customer customer)
        {
            var errorMessage = new string($"Error: CPF {customer.Document} is invalid.");
            if (!CpfValidation.Validate(customer.Document))
            {
                throw new Exception(errorMessage);
            }
            else
            {
                return true;
            }
        }
    }
}
