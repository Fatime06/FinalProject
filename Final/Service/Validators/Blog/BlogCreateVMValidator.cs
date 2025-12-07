using FluentValidation;
using Service.ViewModels.Blog;

namespace Service.Validators.Blog
{
    public class BlogCreateVMValidator : AbstractValidator<BlogCreateVM>
    {
        public BlogCreateVMValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Blog title is required.")
                .MaximumLength(200).WithMessage("Blog title must not exceed 200 characters.");
            RuleFor(b => b.Description)
                .NotEmpty().WithMessage("Blog description is required.");
            RuleFor(b => b.Image)
                .NotEmpty().WithMessage("Blog main image is required.");
            RuleFor(b => b.Image.FileName)
                .MaximumLength(200).WithMessage("Blog main image file name must not exceed 200 characters.");
        }
    }
}
