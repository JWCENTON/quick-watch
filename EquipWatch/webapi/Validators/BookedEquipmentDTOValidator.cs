using DTO.BookedEquipmentDTOs;
using FluentValidation;

namespace webapi.Validators;

public abstract class BaseBookedEquipmentDTOValidator<T> : AbstractValidator<T> where T : BaseBookedEquipmentDTO
{
    protected BaseBookedEquipmentDTOValidator()
    {
        RuleFor(dto => dto.CommissionId).SetValidator(new CommissionIdValidator());

        RuleFor(dto => dto.EquipmentInUseId).SetValidator(new CheckOutIdDTOValidator());
    }
}
public class CreateBookedEquipmentDTOValidator : AbstractValidator<CreateBookedEquipmentDTO>
{
    public CreateBookedEquipmentDTOValidator()
    {
        RuleFor(dto => dto.CommissionId).NotEmpty().WithMessage("You have to select a commission.");
        RuleFor(dto => dto.EquipmentId).SetValidator(new EquipmentIdValidator());
        RuleFor(dto => dto.EndTime)
            .NotNull()
            .WithMessage("You need to specify end date.")
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage("End time must cannot be in the past.");
    }
}

public class FullBookedEquipmentDTOValidator : BaseBookedEquipmentDTOValidator<FullBookedEquipmentDTO>
{
    public FullBookedEquipmentDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new BookedEquipmentIdValidator());
    }
}

public class UpdateBookedEquipmentDTOValidator : AbstractValidator<UpdateBookedEquipmentDTO>
{
    public UpdateBookedEquipmentDTOValidator()
    {
        RuleFor(dto => dto.CommissionId)
            .SetValidator(new CommissionIdValidator()!)
            .When(dto => dto.CommissionId != null);

        RuleFor(dto => dto.EquipmentId)
            .SetValidator(new EquipmentIdValidator()!)
            .When(dto => dto.EquipmentId != null);
    }
}


internal class BookedEquipmentIdValidator : AbstractValidator<string>
{
    internal BookedEquipmentIdValidator()
    {
        RuleFor(id => id)
            .NotEmpty()
            .WithMessage("Check-in ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Check-in ID.");
    }
}