using FluentValidation;
using Service.ViewModels.History;

namespace Service.Validators.History
{
    public class HistoryUpdateVMValidator : AbstractValidator<HistoryUpdateVM>
    {
        public HistoryUpdateVMValidator()
        {
            RuleFor(h => h.Year).InclusiveBetween(1000, DateTime.Now.Year).WithMessage("Year must be between 1000 and current year.");
            RuleFor(h => h.Title).NotEmpty().WithMessage("Title is required.")
                                 .MaximumLength(50).WithMessage("Title must not exceed 50 characters.");
            RuleFor(h => h.Text).NotEmpty().WithMessage("Text is required.")
                                .MaximumLength(200).WithMessage("Text must not exceed 200 characters.");
        }
    }
}
