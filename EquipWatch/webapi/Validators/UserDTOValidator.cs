using DTO.UserDTOs;
using FluentValidation;

namespace DTO.Validators;

public class UserDTOValidator
{
    public class UserIdDTOValidator : AbstractValidator<UserIdDTO>
    {
        public UserIdDTOValidator()
        {
            RuleFor(dto => dto.Id).SetValidator(new UserIdValidator());
        }
    }

    public class FullUserDTOValidator : AbstractValidator<FullUserDTO>
    {
        public FullUserDTOValidator()
        {
            RuleFor(dto => dto.Id).SetValidator(new UserIdValidator());
            RuleFor(dto => dto.UserName).SetValidator(new UserUsernameValidator());
            ApplyCommonRules(this);
        }
    }

    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(dto => dto.FirstName)
                .NotEmpty()
                .WithMessage("First name cannot be empty.")
                .MaximumLength(50)
                .WithMessage("First name cannot be longer than 50 characters"); ;
            RuleFor(dto => dto.LastName)
                .NotEmpty()
                .WithMessage("Last name cannot be empty.")
                .MaximumLength(50)
                .WithMessage("Last name cannot be longer than 50 characters."); ;
            ApplyCommonRules(this);
        }
    }

    public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
    {
        public LoginUserDTOValidator()
        {
            ApplyCommonRules(this);
        }
    }

    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator()
        {
            RuleFor(dto => dto.UserName).SetValidator(new UserUsernameValidator());
            RuleFor(dto => dto.Email).SetValidator(new UserEmailValidator());
        }
    }
    private static void ApplyCommonRules<T>(AbstractValidator<T> validator) where T : BaseUserDTO
    {
        validator.RuleFor(dto => dto.Email).SetValidator(new UserEmailValidator());
        validator.RuleFor(dto => dto.Password).SetValidator(new PasswordValidator());
    }

    private class UserEmailValidator : AbstractValidator<string>
    {
        public UserEmailValidator()
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

    private class UserUsernameValidator : AbstractValidator<string>
    {
        public UserUsernameValidator()
        {
            RuleFor(username => username)
                .NotEmpty()
                .WithMessage("Username cannot be empty.")
                .MaximumLength(50)
                .WithMessage("UserName cannot be longer than 50 characters.");
        }
    }

    private class UserIdValidator : AbstractValidator<string>
    {
        public UserIdValidator()
        {

            RuleFor(userId => userId)
                .NotEmpty()
                .WithMessage("User ID cannot be empty.")
                .Must(GuidValidator.ValidateGuid)
                .WithMessage("Invalid User ID format.");
        }
    }
    private class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
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
}