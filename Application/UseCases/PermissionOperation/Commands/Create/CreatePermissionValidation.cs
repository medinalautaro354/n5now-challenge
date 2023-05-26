using FluentValidation;

namespace Application.UseCases.V1.PermissionOperation.Commands.Create
{
    public class CreatePermissionValidation : AbstractValidator<CreatePermissionCommand>
    {
        public CreatePermissionValidation()
        {
            RuleFor(f => f.EmployeeForename)
                .NotEmpty()
                .WithMessage("EmployeeForename is required.")
                .MaximumLength(50)
                .WithMessage("EmployeeForename maximum length is 50.");

            RuleFor(f => f.EmployeeSurname)
                .NotEmpty()
                .WithMessage("EmployeeSurname is required.")
                .MaximumLength(50)
                .WithMessage("EmployeeSurname maximum length is 50.");

            RuleFor(f => f.PermissionTypeId)
                .NotNull()
                .WithMessage("PermissionTypeId is required.");
        }
    }
}
