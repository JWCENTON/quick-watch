using DTO.CheckOutDTOs;
using FluentValidation;

namespace DTO.Validators;

public abstract class BaseCheckOutDTOValidator<T> : AbstractValidator<T> where T : BaseCheckOutDTO
{
    protected BaseCheckOutDTOValidator()
    {
        RuleFor(dto => dto.EmployId).SetValidator(new EmployIdValidator());

        RuleFor(dto => dto.EquipmentId).SetValidator(new EquipmentIdValidator());

        RuleFor(dto => dto.Time)
            .NotEmpty()
            .WithMessage("Check-out time cannot be empty.");
    }
}
public class CreateCheckOutDTOValidator : BaseCheckOutDTOValidator<CreateCheckOutDTO>
{
}

public class FullCheckOutDTOValidator : BaseCheckOutDTOValidator<FullCheckOutDTO>
{
    public FullCheckOutDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new CheckOutIdDTOValidator());
    }
}

public class UpdateCheckOutDTOValidator : AbstractValidator<UpdateCheckOutDTO>
{
    public UpdateCheckOutDTOValidator()
    {
        RuleFor(dto => dto.EmployId)
            .SetValidator(new EmployIdValidator()!)
            .When(dto => dto.EmployId != null);

        RuleFor(dto => dto.EquipmentId)
            .SetValidator(new EquipmentIdValidator()!)
            .When(dto => dto.EquipmentId != null);
    }
}


internal class CheckOutIdDTOValidator : AbstractValidator<string>
{
    internal CheckOutIdDTOValidator()
    {
        RuleFor(id => id)
            .NotEmpty()
            .WithMessage("Check-in ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Check-in ID.");
    }
}