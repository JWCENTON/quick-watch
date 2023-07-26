using DTO.WorksOnDTOs;
using FluentValidation;

namespace webapi.Validators;

public abstract class BaseWorksOnDTOValidator<T> : AbstractValidator<T> where T : BaseWorksOnDTO
{
    protected BaseWorksOnDTOValidator()
    {
        RuleFor(dto => dto.CommissionId).SetValidator(new CommissionIdValidator());

        RuleFor(dto => dto.UserId).SetValidator(new UserIdValidator());
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
        RuleFor(dto => dto.CommissionId)
            .SetValidator(new CommissionIdValidator()!)
            .When(dto => dto.CommissionId != null);

        RuleFor(dto => dto.UserId)
            .SetValidator(new UserIdValidator()!)
            .When(dto => dto.UserId != null);
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