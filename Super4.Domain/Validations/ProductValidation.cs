using FluentValidation;
using Super4.Domain.Model;

namespace Super4.Domain.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(x => x.ProductName).NotNull().NotEmpty().WithMessage("Product must have a name.");
            RuleFor(x => x.ProductName).MinimumLength(3).WithMessage("Product name must be greater than 3 characters.");
            RuleFor(x => x.ProductName).MaximumLength(30).WithMessage("Product name must not be greater than 30 characters.");
        }
    }
}
