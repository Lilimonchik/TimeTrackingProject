using FluentValidation;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Validations
{
    public class ActivityDtoValidator : AbstractValidator<ActivityDto>
    {
        public ActivityDtoValidator()
        {
            RuleFor(x => x.Hours)
                .GreaterThan(0).WithMessage("Hours worked must be greater than 0.")
                .LessThanOrEqualTo(24).WithMessage("Hours worked must be less than or equal to 24.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future.");
        }
    }
}
