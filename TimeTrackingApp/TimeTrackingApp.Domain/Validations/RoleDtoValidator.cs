using FluentValidation;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Validations
{
    public class RoleDtoValidator: AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");
        }
    }
}
