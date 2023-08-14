using DTO.CheckOutDTOs;
using FluentValidation;

namespace webapi.Validators;

public abstract class BaseCheckOutDTOValidator<T> : AbstractValidator<T> where T : BaseCheckOutDTO
{
    protected BaseCheckOutDTOValidator()
    {
        RuleFor(dto => dto.UserId).SetValidator(new UserIdValidator());

        RuleFor(dto => dto.EquipmentId).SetValidator(new EquipmentIdValidator());

    }
}
public class CreateCheckOutDTOValidator : BaseCheckOutDTOValidator<CreateCheckOutDTO>
{
    public CreateCheckOutDTOValidator()
    {
        RuleFor(dto => dto.ArriveTime)
            .Null()
            .WithMessage("Check-out arrive time cannot be specified while creating.");
    }
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
        RuleFor(dto => dto.UserId)
            .SetValidator(new UserIdValidator()!)
            .When(dto => dto.UserId != null);

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