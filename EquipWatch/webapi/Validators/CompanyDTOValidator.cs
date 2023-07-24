using DTO.CompanyDTOs;
using DTO.Validators;
using FluentValidation;

public abstract class BaseCompanyDTOValidator<T> : AbstractValidator<T> where T : BaseCompanyDTO
{
    protected BaseCompanyDTOValidator()
    {
        RuleFor(dto => dto.Name).SetValidator(new CompanyNameValidator());
        RuleFor(dto => dto.OwnerId).SetValidator(new UserIdValidator());
    }
}

public class CreateCompanyDTOValidator : BaseCompanyDTOValidator<CreateCompanyDTO>
{
}

public class FullCompanyDTOValidator : BaseCompanyDTOValidator<FullCompanyDTO>
{
    public FullCompanyDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new CompanyIdValidator());
    }
}

public class UpdateCompanyDTOValidator : AbstractValidator<UpdateCompanyDTO>
{
    public UpdateCompanyDTOValidator()
    {
        RuleFor(dto => dto.Name)
            .SetValidator(new CompanyNameValidator()!)
            .When(dto => dto.Name != null);

        RuleFor(dto => dto.OwnerId)
            .SetValidator(new UserIdValidator()!)
            .When(dto => dto.OwnerId != null);
    }
}

internal class CompanyIdValidator : AbstractValidator<string>
{
    internal CompanyIdValidator()
    {
        RuleFor(id => id)
            .NotEmpty()
            .WithMessage("Company id cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Company ID format.");
    }
}
internal class CompanyNameValidator : AbstractValidator<string>
{
    internal CompanyNameValidator()
    {
        RuleFor(name => name)
            .NotEmpty()
            .WithMessage("Company name cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Company name cannot exceed 50 characters.");
    }
}