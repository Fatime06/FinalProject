using FluentValidation;
using Service.ViewModels.Account.User;

namespace Service.Validators.Account
{
    public class ChangePasswordVMValidator : AbstractValidator<ChangePasswordVM>
    {
        public ChangePasswordVMValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Current password is required.");
            RuleFor(u => u.NewPassword)
           .NotEmpty().WithMessage("Password is required.")
           .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
            RuleFor(u => u.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(u => u.NewPassword).WithMessage("Passwords do not match.");
        }
    }
}
