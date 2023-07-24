using Domain.Employee;
using Domain.Invite;
using DTO.CheckInDTOs;
using DTO.EmployDTOs;
using DTO.Validators;
using FluentValidation;

public abstract class BaseEmployDTOValidator<T> : AbstractValidator<T> where T : BaseEmployDTO
{
    protected BaseEmployDTOValidator()
    {
        RuleFor(dto => dto.Company).SetValidator(new CompanyIdDTOValidator());

        RuleFor(dto => dto.UserId).SetValidator(new UserIdDTOValidator());

        RuleFor(dto => dto.Role)
            .NotNull()
            .WithMessage("Role cannot be empty.")
            .IsInEnum()
            .WithMessage($"Invalid role. Role must be one of the following values: {string.Join(", ", Enum.GetNames(typeof(Role)))}");
    }
}

public class EmployIdDTOValidator : AbstractValidator<EmployIdDTO>
{
    public EmployIdDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new EmployeIdValidator());
    }
}

public class CreateEmployDTOValidator : BaseEmployDTOValidator<CreateEmployDTO>
{
}

public class FullEmployDTOValidator : BaseEmployDTOValidator<FullEmployDTO>
{
    public FullEmployDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new EmployeIdValidator());
    }
}

public class UpdateEmployDTOValidator : AbstractValidator<UpdateEmployDTO>
{
    public UpdateEmployDTOValidator()
    {
        RuleFor(dto => dto.Company)
            .SetValidator(new CompanyIdDTOValidator()!)
            .When(dto => dto.Company != null);

        RuleFor(dto => dto.UserId)
            .SetValidator(new UserIdDTOValidator()!)
            .When(dto => dto.UserId != null);

        RuleFor(dto => dto.Role)
            .IsInEnum()
            .WithMessage($"Invalid role. Role must be one of the following values: {string.Join(", ", Enum.GetNames(typeof(Role)))}")
            .When(dto => dto.Role != null);
    }
}

internal class EmployeIdValidator : AbstractValidator<string>
{
    internal EmployeIdValidator()
    {
        RuleFor(id => id)
            .NotEmpty()
            .WithMessage("Employee ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Employee ID.");
    }
}