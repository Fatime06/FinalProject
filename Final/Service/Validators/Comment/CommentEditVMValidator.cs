using FluentValidation;
using Service.ViewModels.Comment;

namespace Service.Validators.Comment
{
    public class CommentEditVMValidator : AbstractValidator<CommentEditVM>
    {
        public CommentEditVMValidator()
        {
            RuleFor(c => c.BlogId)
               .NotEmpty().WithMessage("BlogId is required.")
               .GreaterThan(0).WithMessage("BlogId must be greater than zero.");
            RuleFor(c => c.Text)
                .NotEmpty().WithMessage("Text is required.")
                .MaximumLength(1000).WithMessage("Text cannot exceed 1000 characters.");
        }
    }
}
