using FluentValidation;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Validations
{
    public class ProjectDtoValidator: AbstractValidator<ProjectDto>
    {
        public ProjectDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start Date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Start Date cannot be in the future.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End Date is required.")
                .GreaterThan(x => x.StartDate).WithMessage("End Date must be after the Start Date.");
        }
    }
}
