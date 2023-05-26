using FluentValidation;

namespace Application.UseCases.V1.PermissionOperation.Commands.Update
{
    public class UpdatePermissionValidation : AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionValidation()
        {
            RuleFor(f => f.Id)
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0.");

            RuleFor(f => f.EmployeeSurname)
                .NotEmpty()
                .WithMessage("EmployeeSurname is required.")
                .MaximumLength(50)
                .WithMessage("Maximum length is 50");

            RuleFor(f => f.EmployeeForename)
                .NotEmpty()
                .WithMessage("EmployeeForename is required.")
                .MaximumLength(50)
                .WithMessage("Maximum length is 50");

            RuleFor(f => f.PermissionTypeId)
                .GreaterThan(0)
                .WithMessage("PermissionTypeId must be greater than 0.");
        }
    }
}
