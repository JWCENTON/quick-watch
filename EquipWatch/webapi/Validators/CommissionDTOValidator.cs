using DTO.CommissionDTOs;
using FluentValidation;

namespace webapi.Validators;

public abstract class BaseCommissionDTOValidator<T> : AbstractValidator<T> where T : BaseCommissionDTO
{
    protected BaseCommissionDTOValidator()
    {
        RuleFor(dto => dto.CompanyId).SetValidator(new CompanyIdValidator());

        RuleFor(dto => dto.ClientId).SetValidator(new ClientIdValidator());

        RuleFor(dto => dto.Location)
            .SetValidator(new CommissionLocationValidator());

        RuleFor(dto => dto.Description)
            .SetValidator(new CommissionDescriptionValidator());

        RuleFor(dto => dto.Scope)
            .SetValidator(new CommissionScopeValidator());

        RuleFor(dto => dto.StartTime)
            .NotNull()
            .WithMessage("Start time cannot be empty.");

        RuleFor(dto => dto.EndTime)
            .NotNull()
            .WithMessage("End time cannot be empty.")
            .GreaterThan(dto => dto.StartTime)
            .WithMessage("End time must be after start time.");
    }
}

public class CreateCommissionDTOValidator : BaseCommissionDTOValidator<CreateCommissionDTO>
{
}

public class FullCommissionDTOValidator : BaseCommissionDTOValidator<FullCommissionDTO>
{
    public FullCommissionDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new CommissionIdValidator());
    }
}

public class UpdateCommissionDTOValidator : AbstractValidator<UpdateCommissionDTO>
{
    public UpdateCommissionDTOValidator()
    {
        RuleFor(dto => dto.CompanyId)
            .SetValidator(new CompanyIdValidator()!)
            .When(dto => dto.CompanyId != null);

        RuleFor(dto => dto.ClientId)
            .SetValidator(new ClientIdValidator()!)
            .When(dto => dto.ClientId != null);

        RuleFor(dto => dto.Location)
            .SetValidator(new CommissionLocationValidator()!)
            .When(dto => dto.Location != null);

        RuleFor(dto => dto.Description)
            .SetValidator(new CommissionDescriptionValidator()!)
            .When(dto => dto.Description != null);

        RuleFor(dto => dto.Scope)
            .SetValidator(new CommissionScopeValidator()!)
            .When(dto => dto.Scope != null);

        RuleFor(dto => dto.EndTime)
            .GreaterThan(dto => dto.StartTime)
            .WithMessage("End time must be after start time.")
            .When(dto => dto.StartTime != null && dto.EndTime != null);
    }
}

internal class CommissionIdValidator : AbstractValidator<string>
{
    internal CommissionIdValidator()
    {
        RuleFor(id => id)
            .NotEmpty()
            .WithMessage("Commission ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Commission ID.");
    }
}

internal class CommissionLocationValidator : AbstractValidator<string>
{
    internal CommissionLocationValidator()
    {
        RuleFor(location => location)
            .NotEmpty()
            .WithMessage("Location cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Location cannot exceed 50 characters.");
    }
}

internal class CommissionDescriptionValidator : AbstractValidator<string>
{
    internal CommissionDescriptionValidator()
    {
        RuleFor(description => description)
            .NotEmpty()
            .WithMessage("Description cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Description cannot exceed 50 characters.");
    }
}

internal class CommissionScopeValidator : AbstractValidator<string>
{
    internal CommissionScopeValidator()
    {
        RuleFor(scope => scope)
            .NotEmpty()
            .WithMessage("Scope cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Scope cannot exceed 50 characters.");
    }
}