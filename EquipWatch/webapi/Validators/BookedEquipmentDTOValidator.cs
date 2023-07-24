using DTO.BookedEquipmentDTOs;
using DTO.Validators;
using FluentValidation;

public abstract class BaseBookedEquipmentDTOValidator<T> : AbstractValidator<T> where T : BaseBookedEquipmentDTO
{
    protected BaseBookedEquipmentDTOValidator()
    {
        RuleFor(dto => dto.Commission).SetValidator(new CommissionIdDTOValidator());

        RuleFor(dto => dto.Equipment).SetValidator(new EquipmentIdDTOValidator());
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
        RuleFor(dto => dto.Commission)
            .SetValidator(new CommissionIdDTOValidator()!)
            .When(dto => dto.Commission != null);

        RuleFor(dto => dto.Equipment)
            .SetValidator(new EquipmentIdDTOValidator()!)
            .When(dto => dto.Equipment != null);
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