using DTO.EquipmentDTOs;
using DTO.Validators;
using FluentValidation;

public class EquipmentDTOValidator
{
    public class EquipmentIdDTOValidator : AbstractValidator<EquipmentIdDTO>
    {
        public EquipmentIdDTOValidator()
        {
            RuleFor(dto => dto.Id).SetValidator(new EquipmentIdValidator());
        }
    }

    public class CreateEquipmentDTOValidator : AbstractValidator<CreateEquipmentDTO>
    {
        public CreateEquipmentDTOValidator()
        {
            ApplyCommonRules(this);
            RuleFor(dto => dto.SerialNumber).SetValidator(new EquipmentSerialNumberValidator());
            When(dto => dto.Company != null, () =>
            {
                RuleFor(dto => dto.Company).SetValidator(new CompanyDTOValidator.CompanyIdDTOValidator());
            });
        }
    }

    public class FullEquipmentDTOValidator : AbstractValidator<FullEquipmentDTO>
    {
        public FullEquipmentDTOValidator()
        {
            ApplyCommonRules(this);
            RuleFor(dto => dto.Id).SetValidator(new EquipmentIdValidator());
            RuleFor(dto => dto.SerialNumber).SetValidator(new EquipmentSerialNumberValidator());
            When(dto => dto.Company != null, () =>
            {
                RuleFor(dto => dto.Company).SetValidator(new CompanyDTOValidator.CompanyIdDTOValidator());
            });
        }
    }

    public class PartialEquipmentDTOValidator : AbstractValidator<PartialEquipmentDTO>
    {
        public PartialEquipmentDTOValidator()
        {
            ApplyCommonRules(this);
            RuleFor(dto => dto.Id).SetValidator(new EquipmentIdValidator());
            RuleFor(dto => dto.SerialNumber).SetValidator(new EquipmentSerialNumberValidator());
        }
    }

    public class UpdateEquipmentDTOValidator : AbstractValidator<UpdateEquipmentDTO>
    {
        public UpdateEquipmentDTOValidator()
        {
            ApplyCommonRules(this);
            When(dto => dto.Company != null, () =>
            {
                RuleFor(dto => dto.Company)
                    .SetValidator(new CompanyDTOValidator.CompanyIdDTOValidator());
            });
        }
    }

    private static void ApplyCommonRules<T>(AbstractValidator<T> validator) where T : BaseEquipmentDTO
    {
        validator.RuleFor(dto => dto.Category)
            .NotEmpty()
            .WithMessage("Category cannot be empty.");
        validator.RuleFor(dto => dto.Location)
            .NotEmpty()
            .WithMessage("Location cannot be empty.");
        validator.RuleFor(dto => dto.Condition)
            .NotNull()
            .WithMessage("Condition cannot be empty.");
    }
    private class EquipmentSerialNumberValidator : AbstractValidator<string>
    {
        public EquipmentSerialNumberValidator()
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
    private class EquipmentIdValidator : AbstractValidator<Guid>
    {
        public EquipmentIdValidator()
        {
            RuleFor(id => id.ToString())
                .NotEmpty()
                .WithMessage("Equipment ID cannot be empty.")
                .Must(GuidValidator.ValidateGuid)
                .WithMessage("Invalid Equipment ID format.");
        }
    }
}