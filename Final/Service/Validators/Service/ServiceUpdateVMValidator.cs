using FluentValidation;
using Service.ViewModels.Service;

namespace Service.Validators.Service
{
    public class ServiceUpdateVMValidator : AbstractValidator<ServiceUpdateVM>
    {
        public ServiceUpdateVMValidator()
        {
            RuleFor(s => s.Icon)
                    .NotEmpty().WithMessage("Icon is required.")
                    .MaximumLength(100).WithMessage("Icon must not exceed 100 characters.");
            RuleFor(s => s.Text)
                    .NotEmpty().WithMessage("Text is required.")
                    .MaximumLength(100).WithMessage("Text must not exceed 100 characters.");
        }
    }
}
