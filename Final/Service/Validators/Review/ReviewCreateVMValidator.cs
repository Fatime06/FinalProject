using FluentValidation;
using Service.ViewModels.Review;

namespace Service.Validators.Review
{
    public class ReviewCreateVMValidator : AbstractValidator<ReviewCreateVM>
    {
        public ReviewCreateVMValidator()
        {
            RuleFor(r => r.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5.");
            RuleFor(r => r.Text)
                .NotEmpty()
                .WithMessage("Review text cannot be empty.")
                .MaximumLength(1000)
                .WithMessage("Review text cannot exceed 1000 characters.");
        }
    }
}
