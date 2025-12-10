using FluentValidation;
using Service.ViewModels.Account.User;

namespace Service.Validators.Account
{
    public class PasswordResetVMValidator : AbstractValidator<PasswordResetVM>
    {
        public PasswordResetVMValidator()
        {
            RuleFor(u => u.Password)
           .NotEmpty().WithMessage("Password is required.")
           .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
            RuleFor(u => u.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(u => u.Password).WithMessage("Passwords do not match.");
        }
    }
}
