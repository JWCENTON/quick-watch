using DTO.Validators;
using DTO.WorksOnDTOs;
using FluentValidation;


public abstract class BaseWorksOnDTOValidator<T> : AbstractValidator<T> where T : BaseWorksOnDTO
{
    protected BaseWorksOnDTOValidator()
    {
        RuleFor(dto => dto.Commission).SetValidator(new CommissionIdDTOValidator());

        RuleFor(dto => dto.Employee).SetValidator(new EmployIdDTOValidator());
    }
}
public class CreateWorksOnDTOValidator : BaseWorksOnDTOValidator<CreateWorksOnDTO>
{
}

public class FullWorksOnDTOValidator : BaseWorksOnDTOValidator<FullWorksOnDTO>
{
    public FullWorksOnDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new WorksOnIdDTOValidator());
    }
}

public class UpdateWorksOnDTOValidator : AbstractValidator<UpdateWorksOnDTO>
{
    public UpdateWorksOnDTOValidator()
    {
        RuleFor(dto => dto.Commission)
            .SetValidator(new CommissionIdDTOValidator()!)
            .When(dto => dto.Commission != null);

        RuleFor(dto => dto.Employee)
            .SetValidator(new EmployIdDTOValidator()!)
            .When(dto => dto.Employee != null);
    }
}


internal class WorksOnIdDTOValidator : AbstractValidator<string>
{
    internal WorksOnIdDTOValidator()
    {
        RuleFor(id => id)
            .NotEmpty()
            .WithMessage("Check-in ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Check-in ID.");
    }
}