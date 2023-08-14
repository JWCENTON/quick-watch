using DTO.CheckInDTOs;
using FluentValidation;

namespace webapi.Validators;

public abstract class BaseCheckInDTOValidator<T> : AbstractValidator<T> where T : BaseCheckInDTO
{
    protected BaseCheckInDTOValidator()
    {
        RuleFor(dto => dto.UserId).SetValidator(new UserIdValidator());

        RuleFor(dto => dto.EquipmentId).SetValidator(new EquipmentIdValidator());
    }
}
public class CreateCheckInDTOValidator : BaseCheckInDTOValidator<CreateCheckInDTO>
{
    protected CreateCheckInDTOValidator()
    {
        RuleFor(dto => dto.ArriveTime)
            .Null()
            .WithMessage("Check-in arrive time cannot be specified while creating.");
    }
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
        RuleFor(dto => dto.UserId)
            .SetValidator(new UserIdValidator()!)
            .When(dto => dto.UserId != null);

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