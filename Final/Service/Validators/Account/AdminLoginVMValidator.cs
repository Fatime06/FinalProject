using FluentValidation;
using Service.ViewModels.Account.Admin;

namespace Service.Validators.Account
{
    public class AdminLoginVMValidator : AbstractValidator<AdminLoginVM>
    {
        public AdminLoginVMValidator()
        {
            RuleFor(u => u.Email)
             .NotEmpty().WithMessage("Email is required.")
             .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        }
    }
}
