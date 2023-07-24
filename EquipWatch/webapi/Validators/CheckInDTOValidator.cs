using DTO.CheckInDTOs;
using FluentValidation;

namespace DTO.Validators;

public abstract class BaseCheckInDTOValidator<T> : AbstractValidator<T> where T : BaseCheckInDTO
{
    protected BaseCheckInDTOValidator()
    {
        RuleFor(dto => dto.Employ).SetValidator(new EmployIdDTOValidator());

        RuleFor(dto => dto.Equipment).SetValidator(new EquipmentIdDTOValidator());

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
        RuleFor(dto => dto.Employ)
            .SetValidator(new EmployIdDTOValidator()!)
            .When(dto => dto.Employ != null);

        RuleFor(dto => dto.Equipment)
            .SetValidator(new EquipmentIdDTOValidator()!)
            .When(dto => dto.Equipment != null);
    }
}


internal class CheckInIdDTOValidator : AbstractValidator<Guid>
{
    internal CheckInIdDTOValidator()
    {
        RuleFor(id => id.ToString())
            .NotEmpty()
            .WithMessage("Check-in ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Check-in ID.");
    }
}