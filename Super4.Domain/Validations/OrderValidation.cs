using FluentValidation;
using Super4.Domain.Model;

namespace Super4.Domain.Validations
{
    public class OrderValidation : AbstractValidator<Order>
    {
        public OrderValidation()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Items.Count).NotEmpty().NotNull().NotEqual(0).WithMessage("Must add at least one item");
            //RuleFor(x => x.Customer.Id).NotEmpty().NotNull().WithMessage("Must add an customer");
            //RuleFor(x => x.Stock.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity must be a positive value.");
            //RuleFor(x => x.Stock.Quantity).NotEmpty().NotNull().WithMessage("Stock can't be empty");
        }
    }
}
