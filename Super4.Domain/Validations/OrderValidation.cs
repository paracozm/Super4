using FluentValidation;
using Super4.Domain.Model;

namespace Super4.Domain.Validations
{
    public class OrderValidation : AbstractValidator<Order>
    {
        public OrderValidation()
        {
            //RuleFor(x => x.Items).NotEmpty().WithMessage("Must add at least one item");
            //RuleFor(x => x.Customer.Id).NotEmpty().NotNull().WithMessage("Must add an customer");
           // RuleFor(x => x.Stocks).NotEmpty().WithMessage("Stock can't be empty");
        }
    }
}
