using Domain.Employee;
using DTO.EmployDTOs;
using FluentValidation;

namespace webapi.Validators;

public abstract class BaseEmployDTOValidator<T> : AbstractValidator<T> where T : BaseEmployDTO
{
    protected BaseEmployDTOValidator()
    {
        RuleFor(dto => dto.CompanyId).SetValidator(new CompanyIdValidator());

        RuleFor(dto => dto.UserId).SetValidator(new UserIdValidator());

        RuleFor(dto => dto.Role)
            .NotNull()
            .WithMessage("Role cannot be empty.")
            .IsInEnum()
            .WithMessage($"Invalid role. Role must be one of the following values: {string.Join(", ", Enum.GetNames(typeof(Role)))}");
    }
}

public class CreateEmployDTOValidator : BaseEmployDTOValidator<CreateEmployDTO>
{
}

public class FullEmployDTOValidator : BaseEmployDTOValidator<FullEmployDTO>
{
    public FullEmployDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new EmployIdValidator());
    }
}

public class UpdateEmployDTOValidator : AbstractValidator<UpdateEmployDTO>
{
    public UpdateEmployDTOValidator()
    {
        RuleFor(dto => dto.CompanyId)
            .SetValidator(new CompanyIdValidator()!)
            .When(dto => dto.CompanyId != null);

        RuleFor(dto => dto.UserId)
            .SetValidator(new UserIdValidator()!)
            .When(dto => dto.UserId != null);

        RuleFor(dto => dto.Role)
            .IsInEnum()
            .WithMessage($"Invalid role. Role must be one of the following values: {string.Join(", ", Enum.GetNames(typeof(Role)))}")
            .When(dto => dto.Role != null);
    }
}

internal class EmployIdValidator : AbstractValidator<string>
{
    internal EmployIdValidator()
    {
        RuleFor(id => id)
            .NotEmpty()
            .WithMessage("Employee ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Employee ID.");
    }
}