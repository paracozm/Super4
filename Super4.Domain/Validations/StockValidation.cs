using FluentValidation;
using Super4.Domain.Model;

namespace Super4.Domain.Validations
{
    public class StockValidation : AbstractValidator<Stock>
    {
        public StockValidation()
        {
            RuleFor(x => x.Quantity).NotNull().NotEmpty().WithMessage("Stock can't be null.");
            RuleFor(x => x.Product.Id).NotEmpty().NotNull().WithMessage("Must link a product ID to the stock.");
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity must be a positive value.");
        }
    }
}
