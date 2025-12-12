using FluentValidation;
using Service.ViewModels.Brand;

namespace Service.Validators.Brand
{
    public class BrandUpdateVMValidator : AbstractValidator<BrandUpdateVM>
    {
        public BrandUpdateVMValidator()
        {
            RuleFor(b => b.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        }
    }
}
