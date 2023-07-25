using DTO.EquipmentDTOs;
using DTO.Validators;
using FluentValidation;

public abstract class BaseEquipmentDTOValidator<T> : AbstractValidator<T> where T : BaseEquipmentDTO
{
    protected BaseEquipmentDTOValidator()
    {
        RuleFor(dto => dto.Category).SetValidator(new EquipmentCategoryValidator());
        
        RuleFor(dto => dto.Location).SetValidator(new EquipmentLocationValidator());

        RuleFor(dto => dto.Condition)
            .NotNull()
            .WithMessage("Condition cannot be empty.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Condition cannot be less than 0.")
            .LessThanOrEqualTo(5)
            .WithMessage("Condition cannot be higher than 5.");
    }
}

public class CreateEquipmentDTOValidator : BaseEquipmentDTOValidator<CreateEquipmentDTO>
{
    public CreateEquipmentDTOValidator()
    {
        RuleFor(dto => dto.SerialNumber).SetValidator(new EquipmentSerialNumberValidator());
        
        RuleFor(dto => dto.CompanyId).SetValidator(new CompanyIdValidator());
    }
}

public class FullEquipmentDTOValidator : BaseEquipmentDTOValidator<FullEquipmentDTO>
{
    public FullEquipmentDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new EquipmentIdValidator());

        RuleFor(dto => dto.SerialNumber).SetValidator(new EquipmentSerialNumberValidator());
        
        RuleFor(dto => dto.CompanyId).SetValidator(new CompanyIdValidator());
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

public class UpdateEquipmentDTOValidator : AbstractValidator<UpdateEquipmentDTO>
{
    public UpdateEquipmentDTOValidator()
    {
        RuleFor(dto => dto.SerialNumber)
            .SetValidator(new EquipmentSerialNumberValidator()!)
            .When(dto => dto.SerialNumber != null);

        RuleFor(dto => dto.CompanyId.ToString())
            .SetValidator(new EquipmentIdValidator()!)
            .When(dto => dto.CompanyId != null);

        RuleFor(dto => dto.Category)
            .SetValidator(new EquipmentCategoryValidator()!)
            .When(dto => dto.Category != null);

        RuleFor(dto => dto.Location)
            .SetValidator(new EquipmentLocationValidator()!)
            .When(dto => dto.Location != null);

        RuleFor(dto => dto.Condition)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Condition cannot be less than 1.")
            .LessThanOrEqualTo(5)
            .WithMessage("Condition cannot be higher than 5.")
            .When(dto => dto.Condition != null);
    }
}
internal class EquipmentSerialNumberValidator : AbstractValidator<string>
{
    internal EquipmentSerialNumberValidator()
    {
        RuleFor(serialNumber => serialNumber)
            .NotEmpty()
            .WithMessage("Serial number cannot be empty.")
            .MinimumLength(1)
            .WithMessage("Serial number cannot be shorter than 1 characters")
            .MaximumLength(30)
            .WithMessage("Serial number cannot exceed 30 characters.");
    }
}
internal class EquipmentIdValidator : AbstractValidator<string>
{
    internal EquipmentIdValidator()
    {
        RuleFor(id => id)
            .NotEmpty()
            .WithMessage("Equipment ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Equipment ID format.");
    }
}

internal class EquipmentCategoryValidator : AbstractValidator<string>
{
    internal EquipmentCategoryValidator()
    {
        RuleFor(category => category)
            .NotEmpty()
            .WithMessage("Category cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Category cannot exceed 50 characters.");
        
    }
}

internal class EquipmentLocationValidator : AbstractValidator<string>
{
    internal EquipmentLocationValidator()
    {
        RuleFor(location => location)
            .NotEmpty()
            .WithMessage("Location cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Location cannot exceed 50 characters.");
    }
}