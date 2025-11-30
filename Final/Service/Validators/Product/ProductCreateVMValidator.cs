using FluentValidation;
using Service.ViewModels.Product;

namespace Service.Validators.Product
{
    public class ProductCreateVMValidator : AbstractValidator<ProductCreateVM>
    {
        public ProductCreateVMValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 35 characters.");
            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");
            RuleFor(p => p.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Product quantity cannot be negative.");
            RuleFor(p=>p.DiscountPrice)
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");
        }
    }
}
