using FluentValidation;
using Service.ViewModels.Brand;

namespace Service.Validators.Brand
{
    public class BrandCreateVMValidator : AbstractValidator<BrandCreateVM>
    {
        public BrandCreateVMValidator()
        {
            RuleFor(b => b.Image)
                .NotNull().WithMessage("Image is required.");
        }
    }
}
