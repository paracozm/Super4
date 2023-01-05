using FluentValidation;
using Super4.Domain.Model;

namespace Super4.Domain.Validations
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("First name can't be null or empty.");
            RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("First name is too short (3 or less)");
            RuleFor(x => x.FirstName).MaximumLength(20).WithMessage("First name is too big (20 or more)");

            RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("Last name can't be null or empty.");
            RuleFor(x => x.LastName).MinimumLength(2).WithMessage("Last name is too short (1 or less)");
            RuleFor(x => x.LastName).MaximumLength(20).WithMessage("Last name is too big (20 or more)");

            RuleFor(x => x.Document.Replace("-", "").Replace(".", "")).NotEmpty().NotNull().WithMessage("Document can't be null or empty.");
            RuleFor(x => x.Document.Replace("-", "").Replace(".", "")).Length(11,11).WithMessage("Document must have 11 characters only.");
            
            RuleFor(x => x.CEP.Replace("-", "").Replace(".", "")).NotNull().NotEmpty().WithMessage("CEP can't be null or empty.");
            RuleFor(x => x.CEP.Replace("-", "").Replace(".", "")).Length(8, 8).WithMessage("CEP must have 8 characters only.");

            RuleFor(x => x.AddressNumber).NotNull().NotEmpty().WithMessage("Address number can't be null or empty.");
            RuleFor(x => x.AddressNumber).Length(1, 6).WithMessage("Address number is too big. Maximum is 6 characters.");
            RuleFor(x => x.AddressNumber).NotEqual("string").WithMessage("Address number can't be null or empty.");
        }
    }
}
