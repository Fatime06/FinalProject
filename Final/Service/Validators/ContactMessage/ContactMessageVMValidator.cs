using FluentValidation;
using Service.ViewModels.ContactMessage;

namespace Service.Validators.ContactMessage
{
    public class ContactMessageVMValidator : AbstractValidator<ContactMessageCreateVM>
    {
        public ContactMessageVMValidator()
        {
            RuleFor(x => x.Name)
          .NotEmpty().WithMessage("Name is required.")
          .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.")
                .MaximumLength(150);

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message cannot be empty.")
                .MaximumLength(1000);
        }
    }
}
