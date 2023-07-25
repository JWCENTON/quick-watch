using DTO.CheckInDTOs;
using FluentValidation;

namespace webapi.Validators;

public abstract class BaseCheckInDTOValidator<T> : AbstractValidator<T> where T : BaseCheckInDTO
{
    protected BaseCheckInDTOValidator()
    {
        RuleFor(dto => dto.EmployId).SetValidator(new EmployIdValidator());

        RuleFor(dto => dto.EquipmentId).SetValidator(new EquipmentIdValidator());

        RuleFor(dto => dto.Time)
            .NotEmpty()
            .WithMessage("Check-in time cannot be empty.");
    }
}
public class CreateCheckInDTOValidator : BaseCheckInDTOValidator<CreateCheckInDTO>
{
}

public class FullCheckInDTOValidator : BaseCheckInDTOValidator<FullCheckInDTO>
{
    public FullCheckInDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new CheckInIdDTOValidator());
    }
}

public class UpdateCheckInDTOValidator : AbstractValidator<UpdateCheckInDTO>
{
    public UpdateCheckInDTOValidator()
    {
        RuleFor(dto => dto.EmployId)
            .SetValidator(new EmployIdValidator()!)
            .When(dto => dto.EmployId != null);

        RuleFor(dto => dto.EquipmentId)
            .SetValidator(new EquipmentIdValidator()!)
            .When(dto => dto.EquipmentId != null);
    }
}


internal class CheckInIdDTOValidator : AbstractValidator<string>
{
    internal CheckInIdDTOValidator()
    {
        RuleFor(id => id)
            .NotEmpty()
            .WithMessage("Check-in ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Check-in ID.");
    }
}