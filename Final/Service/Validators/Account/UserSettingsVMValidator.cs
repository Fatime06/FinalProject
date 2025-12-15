using FluentValidation;
using Service.ViewModels.Account.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators.Account
{
    public class UserSettingsVMValidator : AbstractValidator<UserSettingsVM>
    {
        public UserSettingsVMValidator()
        {
            RuleFor(u => u.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 200 characters.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.");
            RuleFor(u => u.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .MaximumLength(100).WithMessage("Surname must not exceed 100 characters.")
                .MinimumLength(3).WithMessage("Surname must be at least 3 characters long.");
            RuleFor(u => u.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(250).WithMessage("Address must not exceed 250 characters.")
                .MinimumLength(3).WithMessage("Address must be at least 3 characters long.");
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
