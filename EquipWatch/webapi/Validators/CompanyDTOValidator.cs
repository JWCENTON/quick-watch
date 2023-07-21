using DTO.CompanyDTOs;
using DTO.Validators;
using FluentValidation;


public class CompanyDTOValidator
{
    public class CompanyIdDTOValidator : AbstractValidator<CompanyIdDTO>
    {
        public CompanyIdDTOValidator()
        {
            RuleFor(dto => dto.Id).SetValidator(new CompanyIdValidator());
        }
    }

    public class CreateCompanyDTOValidator : AbstractValidator<CreateCompanyDTO>
    {
        public CreateCompanyDTOValidator()
        {
            ApplyCommonRules(this);
        }
    }

    public class FullCompanyDTOValidator : AbstractValidator<FullCompanyDTO>
    {
        public FullCompanyDTOValidator()
        {
            RuleFor(dto => dto.Id).SetValidator(new CompanyIdValidator());
            ApplyCommonRules(this);
        }
    }

    public class UpdateCompanyDTOValidator : AbstractValidator<UpdateCompanyDTO>
    {
        public UpdateCompanyDTOValidator()
        {
            ApplyCommonRules(this);
        }
    }
    private static void ApplyCommonRules<T>(AbstractValidator<T> validator) where T : BaseCompanyDTO
    {
        validator.RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("Company name cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Company name cannot exceed 50 characters.");
        validator.RuleFor(dto => dto.OwnerId).SetValidator(new UserDTOValidator.UserIdDTOValidator());
    }
    private class CompanyIdValidator : AbstractValidator<Guid>
    {
        public CompanyIdValidator()
        {
            RuleFor(id => id.ToString())
                .NotEmpty()
                .WithMessage("Company id cannot be empty.")
                .Must(GuidValidator.ValidateGuid)
                .WithMessage("Invalid Company ID format.");
        }
    }
}