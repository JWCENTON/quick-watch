using DTO.BookedEquipmentDTOs;
using DTO.Validators;
using FluentValidation;

public abstract class BaseBookedEquipmentDTOValidator<T> : AbstractValidator<T> where T : BaseBookedEquipmentDTO
{
    protected BaseBookedEquipmentDTOValidator()
    {
        RuleFor(dto => dto.CommissionId).SetValidator(new CommissionIdValidator());

        RuleFor(dto => dto.EquipmentId).SetValidator(new EquipmentIdValidator());
    }
}
public class CreateBookedEquipmentDTOValidator : BaseBookedEquipmentDTOValidator<CreateBookedEquipmentDTO>
{
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