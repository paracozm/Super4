using FluentValidation;
using Super4.Domain.Model;

namespace Super4.Domain.Validations
{
    public class OrderValidation : AbstractValidator<Order>
    {
        public OrderValidation()
        {
            RuleFor(x => x.Item).NotEmpty().WithMessage("Must add at least an item");
            RuleFor(x => x.Customer.Id).NotEmpty().NotNull().WithMessage("Must link a customer");
        }
    }
}
