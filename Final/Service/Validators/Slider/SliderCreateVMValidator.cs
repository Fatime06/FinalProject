using FluentValidation;
using Service.ViewModels.Slider;

namespace Service.Validators.Slider
{
    public class SliderCreateVMValidator : AbstractValidator<SliderCreateVM>
    {
        public SliderCreateVMValidator()
        {
            RuleFor(s=>s.SmallNote)
                .MaximumLength(50).WithMessage("SmallNote length must be less than 50 characters.");
            RuleFor(s => s.SmallText)
                .NotEmpty().WithMessage("SmallText is required.")
                .MaximumLength(25).WithMessage("SmallText length must be less than 25 characters.");
            RuleFor(s => s.BigText)
                .NotEmpty().WithMessage("BigText is required.")
                .MaximumLength(45).WithMessage("BigText length must be less than 45 characters.");
            RuleFor(s => s.MediumText)
                .NotEmpty().WithMessage("MediumText is required.")
                .MaximumLength(35).WithMessage("MediumText length must be less than 35 characters.");
            RuleFor(s => s.ButtonText)
                .NotEmpty().WithMessage("ButtonText is required.")
                .MaximumLength(20).WithMessage("ButtonText length must be less than 20 characters.");
            RuleFor(s => s.Image)
                .NotNull().WithMessage("Image is required.");
        }
    }
}
