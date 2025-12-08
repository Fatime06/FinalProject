using FluentValidation;
using Service.ViewModels.Account.User;

namespace Service.Validators.Account
{
    public class UserRegisterVMValidator : AbstractValidator<UserRegisterVM>
    {
        public UserRegisterVMValidator()
        {
            RuleFor(u => u.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 200 characters.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.");
            RuleFor(u=>u.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .MaximumLength(100).WithMessage("Surname must not exceed 100 characters.")
                .MinimumLength(3).WithMessage("Surname must be at least 3 characters long.");
            RuleFor(u => u.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(250).WithMessage("Address must not exceed 250 characters.")
                .MinimumLength(3).WithMessage("Address must be at least 3 characters long.");
            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Username must not exceed 100 characters.");
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
            RuleFor(u => u.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(u => u.Password).WithMessage("Passwords do not match.");
            RuleFor(u => u.Birthday)
                .NotEmpty().WithMessage("Birthday is required.")
                .LessThan(DateTime.Now).WithMessage("Birthday must be in the past.");
            RuleFor(x => x).Custom((x, context) =>
            {
                var today = DateTime.Today;
                var eighteenYearsAgo = today.AddYears(-18);

                if (x.Birthday > eighteenYearsAgo)
                {
                    context.AddFailure("Birthday", "You must be 18 years old to register.");
                }
            });
        }
    }
}
