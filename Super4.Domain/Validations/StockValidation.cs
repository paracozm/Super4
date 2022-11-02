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
        }
    }
}
