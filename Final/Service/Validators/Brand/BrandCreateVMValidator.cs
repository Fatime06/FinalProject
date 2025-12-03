using FluentValidation;
using Service.ViewModels.Brand;

namespace Service.Validators.Brand
{
    public class BrandCreateVMValidator : AbstractValidator<BrandCreateVM>
    {
        public BrandCreateVMValidator()
        {
            RuleFor(b => b.Image)
                .NotNull().WithMessage("Image is required.")
                .Must(file => file != null && (file.ContentType == "image/jpeg" || file.ContentType == "image/png"))
                .WithMessage("Image must be a JPEG or PNG file.");
        }
    }
}
