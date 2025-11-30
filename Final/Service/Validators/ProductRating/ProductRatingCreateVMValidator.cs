using FluentValidation;
using Service.ViewModels.ProductRatingVM;

namespace Service.Validators.ProductRating
{
    public class ProductRatingCreateVMValidator : AbstractValidator<ProductRatingCreateVM>
    {
        public ProductRatingCreateVMValidator()
        {
            RuleFor(pr => pr.Value)
                .InclusiveBetween(1, 5).WithMessage("Value must be between 1 and 5.");
            RuleFor(pr => pr.Comment)
                .MaximumLength(1000).WithMessage("Comment cannot exceed 1000 characters.");
        }
    }
}
