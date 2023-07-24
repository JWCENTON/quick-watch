using Domain.Invite;
using DTO.InviteDTOs;
using DTO.Validators;
using FluentValidation;

public abstract class BaseInviteDTOValidator<T> : AbstractValidator<T> where T : BaseInviteDTO
{
    protected BaseInviteDTOValidator()
    {
        RuleFor(dto => dto.CompanyId).SetValidator(new CompanyIdValidator());
        RuleFor(dto => dto.UserId).SetValidator(new UserIdValidator());

        RuleFor(dto => dto.Status)
            .NotNull()
            .WithMessage("Status cannot be empty.")
            .IsInEnum()
            .WithMessage($"Invalid status. Status must be one of the following values: {string.Join(", ", Enum.GetNames(typeof(Status)))}");

        RuleFor(dto => dto.CreatedAt)
            .NotNull()
            .WithMessage("Created at date cannot be empty.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Created at date cannot be in the future.");
    }
}

public class CreateInviteDTOValidator : BaseInviteDTOValidator<CreateInviteDTO>
{
}

public class FullInviteDTOValidator : BaseInviteDTOValidator<FullInviteDTO>
{
    public FullInviteDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new InviteIdValidator());
    }
}

public class UpdateInviteDTOValidator : AbstractValidator<UpdateInviteDTO>
{
    public UpdateInviteDTOValidator()
    {
        RuleFor(dto => dto.CompanyId)
            .SetValidator(new CompanyIdValidator()!)
            .When(dto => dto.CompanyId != null);

        RuleFor(dto => dto.UserId)
            .SetValidator(new UserIdValidator()!)
            .When(dto => dto.UserId != null);

        RuleFor(dto => dto.Status)
            .IsInEnum()
            .WithMessage($"Invalid status. Status must be one of the following values: {string.Join(", ", Enum.GetNames(typeof(Status)))}")
            .When(dto => dto.Status != null);

        RuleFor(dto => dto.CreatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Created at date cannot be in the future.")
            .When(dto => dto.CreatedAt != null);
    }
}

internal class InviteIdValidator : AbstractValidator<string>
{
    internal InviteIdValidator()
    {
        RuleFor(id => id)
            .NotEmpty()
            .WithMessage("Invite ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Invite ID.");
    }
}