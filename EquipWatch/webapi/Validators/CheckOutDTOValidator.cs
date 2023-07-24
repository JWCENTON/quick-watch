using DTO.CheckOutDTOs;
using FluentValidation;

namespace DTO.Validators;

public abstract class BaseCheckOutDTOValidator<T> : AbstractValidator<T> where T : BaseCheckOutDTO
{
    protected BaseCheckOutDTOValidator()
    {
        RuleFor(dto => dto.Employ).SetValidator(new EmployIdDTOValidator());

        RuleFor(dto => dto.Equipment).SetValidator(new EquipmentIdDTOValidator());

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
        RuleFor(dto => dto.Employ)
            .SetValidator(new EmployIdDTOValidator()!)
            .When(dto => dto.Employ != null);

        RuleFor(dto => dto.Equipment)
            .SetValidator(new EquipmentIdDTOValidator()!)
            .When(dto => dto.Equipment != null);
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