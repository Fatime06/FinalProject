using FluentValidation;
using Service.ViewModels.Account.User;

namespace Service.Validators.Account
{
    public class ForgotPasswordVMValidator : AbstractValidator<ForgotPasswordVM>
    {
        public ForgotPasswordVMValidator()
        {
            RuleFor(u => u.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
