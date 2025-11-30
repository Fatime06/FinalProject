using FluentValidation;
using Service.DTOs.Category;

namespace Service.Validators.Category
{
    public class CategoryUpdateVMValidator : AbstractValidator<CategoryUpdateVM>
    {
        public CategoryUpdateVMValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(25).WithMessage("Category name length must be less than 25 characters.");
        }
    }
}
