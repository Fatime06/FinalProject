using FluentValidation;
using Service.ViewModels.Order;

namespace Service.Validators.Order
{
    public class CheckoutVMValidator : AbstractValidator<CheckoutVM>
    {
        public CheckoutVMValidator()
        {
            RuleFor(c=>c.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(200).WithMessage("Address maximum length is 200 characters");
            RuleFor(c=>c.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name maximum length is 50 characters");
            RuleFor(c=>c.Surname)
                .NotEmpty().WithMessage("Surname is required")
                .MaximumLength(50).WithMessage("Surname maximum length is 50 characters");
            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .MaximumLength(20).WithMessage("Phone maximum length is 20 characters");
        }
    }
}
