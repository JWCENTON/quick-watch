using DTO.CompanyDTOs;
using DTO.Validators;
using FluentValidation;

public abstract class BaseCompanyDTOValidator<T> : AbstractValidator<T> where T : BaseCompanyDTO
{
    protected BaseCompanyDTOValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("Company name cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Company name cannot exceed 50 characters.");
        RuleFor(dto => dto.OwnerId).SetValidator(new UserIdDTOValidator());
    }
}

public class CompanyIdDTOValidator : AbstractValidator<CompanyIdDTO>
{
    public CompanyIdDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new CompanyIdValidator());
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

public class UpdateCompanyDTOValidator : BaseCompanyDTOValidator<UpdateCompanyDTO>
{
}

internal class CompanyIdValidator : AbstractValidator<Guid>
{
    internal CompanyIdValidator()
    {
        RuleFor(id => id.ToString())
            .NotEmpty()
            .WithMessage("Company id cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Company ID format.");
    }
}