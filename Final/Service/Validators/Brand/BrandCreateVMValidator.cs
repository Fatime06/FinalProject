using FluentValidation;
using Service.ViewModels.Brand;

namespace Service.Validators.Brand
{
    public class BrandCreateVMValidator : AbstractValidator<BrandCreateVM>
    {
        public BrandCreateVMValidator()
        {
            RuleFor(b=>b.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(b => b.Image)
                .NotNull().WithMessage("Image is required.");
        }
    }
}
