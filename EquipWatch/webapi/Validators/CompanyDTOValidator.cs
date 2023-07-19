using DTO.CompanyDTOs;
using DTO.UserDTOs;
using DTO.Validators;
using FluentValidation;

public class CompanyDTOValidator
{
    public class CompanyIdDTOValidator : AbstractValidator<CompanyIdDTO>
    {
        public CompanyIdDTOValidator()
        {
            RuleFor(dto => dto.Id).NotEmpty();
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
            RuleFor(dto => dto.Id).NotEmpty();
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
        validator.RuleFor(dto => dto.Name).NotEmpty();
        validator.RuleFor(dto => dto.OwnerId).SetValidator(new UserDTOValidator.UserIdDTOValidator());
    }
}