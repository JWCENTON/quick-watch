using DTO.UserDTOs;
using FluentValidation;


namespace DTO.Validators;

public abstract class BaseUserDTOValidator<T> : AbstractValidator<T> where T : BaseUserDTO
{
    protected BaseUserDTOValidator()
    {
        RuleFor(dto => dto.Email).SetValidator(new UserEmailValidator());
        RuleFor(dto => dto.Password).SetValidator(new UserPasswordValidator());
    }
}

public class FullUserDTOValidator : BaseUserDTOValidator<FullUserDTO>
{
    public FullUserDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new UserIdValidator());
        RuleFor(dto => dto.UserName).SetValidator(new UserUsernameValidator());
    }
}

public class CreateUserDTOValidator : BaseUserDTOValidator<CreateUserDTO>
{
    public CreateUserDTOValidator()
    {
        RuleFor(dto => dto.FirstName)
            .NotEmpty()
            .WithMessage("First name cannot be empty.")
            .MaximumLength(50)
            .WithMessage("First name cannot be longer than 50 characters");
        
        RuleFor(dto => dto.LastName)
            .NotEmpty()
            .WithMessage("Last name cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Last name cannot be longer than 50 characters.");
    }
}

public class LoginUserDTOValidator : BaseUserDTOValidator<LoginUserDTO>
{
}

public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
{
    public UpdateUserDTOValidator()
    {
        RuleFor(dto => dto.UserName).SetValidator(new UserUsernameValidator())
            .When(dto => dto.UserName != null);
        RuleFor(dto => dto.Email).SetValidator(new UserEmailValidator())
            .When(dto => dto.Email != null);
    }
}

internal class UserEmailValidator : AbstractValidator<string>
{
    internal UserEmailValidator()
    {
        RuleFor(email => email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .EmailAddress()
            .WithMessage("Email must contain @.")
            .MaximumLength(50)
            .WithMessage("Email cannot be longer than 50 characters.");
    }
}

internal class UserUsernameValidator : AbstractValidator<string>
{
    internal UserUsernameValidator()
    {
        RuleFor(username => username)
            .NotEmpty()
            .WithMessage("Username cannot be empty.")
            .MaximumLength(50)
            .WithMessage("UserName cannot be longer than 50 characters.");
    }
}

internal class UserIdValidator : AbstractValidator<string>
{
    internal UserIdValidator()
    {

        RuleFor(userId => userId)
            .NotEmpty()
            .WithMessage("User ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid User ID format.");
    }
}

internal class UserPasswordValidator : AbstractValidator<string>
{
    internal UserPasswordValidator()
    {
        RuleFor(password => password)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.")
            .Matches("[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]")
            .WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]")
            .WithMessage("Password must contain at least one number.")
            .Matches(@"[!@#$%^&*()_+{}\[\]:;'"",.<>?\\|`~\/]")
            .WithMessage("Password must contain at least one special character.")
            .Must(password => !password.Contains(' '))
            .WithMessage("Password cannot contain spaces.")
            .MaximumLength(50)
            .WithMessage("Password cannot be longer than 50 characters.");
    }
}