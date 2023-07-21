using DTO.EquipmentDTOs;
using DTO.Validators;
using FluentValidation;

public abstract class BaseEquipmentDTOValidator<T> : AbstractValidator<T> where T : BaseEquipmentDTO
{
    protected BaseEquipmentDTOValidator()
    {
        RuleFor(dto => dto.Category)
            .NotEmpty()
            .WithMessage("Category cannot be empty.");
        RuleFor(dto => dto.Location)
            .NotEmpty()
            .WithMessage("Location cannot be empty.");
        RuleFor(dto => dto.Condition)
            .NotNull()
            .WithMessage("Condition cannot be empty.");
    }
}


public class EquipmentIdDTOValidator : AbstractValidator<EquipmentIdDTO>
{
    public EquipmentIdDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new EquipmentIdValidator());
    }
}

public class CreateEquipmentDTOValidator : BaseEquipmentDTOValidator<CreateEquipmentDTO>
{
    public CreateEquipmentDTOValidator()
    {
        RuleFor(dto => dto.SerialNumber).SetValidator(new EquipmentSerialNumberValidator());
        When(dto => dto.Company != null, () =>
        {
            RuleFor(dto => dto.Company).SetValidator(new CompanyIdDTOValidator());
        });
    }
}

public class FullEquipmentDTOValidator : BaseEquipmentDTOValidator<FullEquipmentDTO>
{
    public FullEquipmentDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new EquipmentIdValidator());
        RuleFor(dto => dto.SerialNumber).SetValidator(new EquipmentSerialNumberValidator());
        When(dto => dto.Company != null, () =>
        {
            RuleFor(dto => dto.Company).SetValidator(new CompanyIdDTOValidator());
        });
    }
}

public class PartialEquipmentDTOValidator : BaseEquipmentDTOValidator<PartialEquipmentDTO>
{
    public PartialEquipmentDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new EquipmentIdValidator());
        RuleFor(dto => dto.SerialNumber).SetValidator(new EquipmentSerialNumberValidator());
    }
}

public class UpdateEquipmentDTOValidator : BaseEquipmentDTOValidator<UpdateEquipmentDTO>
{
    public UpdateEquipmentDTOValidator()
    {
        When(dto => dto.Company != null, () =>
        {
            RuleFor(dto => dto.Company)
                .SetValidator(new CompanyIdDTOValidator());
        });
    }
}
internal class EquipmentSerialNumberValidator : AbstractValidator<string>
{
    internal EquipmentSerialNumberValidator()
    {
        RuleFor(serialNumber => serialNumber)
            .NotEmpty()
            .WithMessage("Serial number cannot be empty.")
            .MinimumLength(10)
            .WithMessage("Serial number cannot be shorter than 10 characters")
            .MaximumLength(30)
            .WithMessage("Serial number cannot exceed 30 characters.");
    }
}
internal class EquipmentIdValidator : AbstractValidator<Guid>
{
    internal EquipmentIdValidator()
    {
        RuleFor(id => id.ToString())
            .NotEmpty()
            .WithMessage("Equipment ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Equipment ID format.");
    }
}