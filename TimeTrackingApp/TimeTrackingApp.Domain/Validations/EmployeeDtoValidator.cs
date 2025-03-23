using FluentValidation;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Validations
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

            RuleFor(x => x.Sex)
                .NotEmpty().WithMessage("Sex is required.")
                .Must(x => x == "Male" || x == "Female").WithMessage("Sex must be either 'Male' or 'Female'.");
        }
    }
}
