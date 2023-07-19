using DTO.UserDTOs;
using FluentValidation;

namespace DTO.Validators;

public class UserDTOValidator
{
    public class UserIdDTOValidator : AbstractValidator<UserIdDTO>
    {
        public UserIdDTOValidator()
        {
            RuleFor(dto => dto.Id).NotEmpty();
        }
    }

    public class FullUserDTOValidator : AbstractValidator<FullUserDTO>
    {
        public FullUserDTOValidator()
        {
            RuleFor(dto => dto.Id).NotEmpty();
            RuleFor(dto => dto.UserName).NotEmpty();
            RuleFor(dto => dto.Email).NotEmpty().EmailAddress();
            RuleFor(dto => dto.Password).NotEmpty().MinimumLength(6);
        }
    }

    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(dto => dto.FirstName).NotEmpty();
            RuleFor(dto => dto.LastName).NotEmpty();
            RuleFor(dto => dto.Email).NotEmpty().EmailAddress();
            RuleFor(dto => dto.Password).NotEmpty().MinimumLength(6);
        }
    }

    public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
    {
        public LoginUserDTOValidator()
        {
            RuleFor(dto => dto.Email).NotEmpty().EmailAddress();
            RuleFor(dto => dto.Password).NotEmpty();
        }
    }

    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator()
        {
            RuleFor(dto => dto.UserName).NotEmpty();
            RuleFor(dto => dto.Email).NotEmpty().EmailAddress();
        }
    }
}