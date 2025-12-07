using FluentValidation;
using Service.ViewModels.Blog;

namespace Service.Validators.Blog
{
    public class BlogUpdateVMValidator : AbstractValidator<BlogUpdateVM>
    {
        public BlogUpdateVMValidator()
        {
            RuleFor(b => b.Title)
           .NotEmpty().WithMessage("Blog title is required.")
           .MaximumLength(200).WithMessage("Blog title must not exceed 200 characters.");
            RuleFor(b => b.Description)
                .NotEmpty().WithMessage("Blog description is required.");
            RuleFor(x => x.CategoryIds)
                .NotNull()
                .WithMessage("Category is required");
            RuleFor(x => x.TagIds)
                .NotEmpty()
                .WithMessage("Tag is required");
        }
    }
}
