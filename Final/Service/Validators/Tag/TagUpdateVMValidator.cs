using FluentValidation;
using Service.ViewModels.Tag;

namespace Service.Validators.Tag
{
    public class TagUpdateVMValidator : AbstractValidator<TagUpdateVM>
    {
        public TagUpdateVMValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Tag name is required.")
                .MaximumLength(100).WithMessage("Tag name must not exceed 100 characters.");
        }
    }
}
