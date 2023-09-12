using FluentValidation;

namespace webapi.Validators;

public class AppConfigValidator : AbstractValidator<IConfiguration>
{
    public AppConfigValidator()
    {
        RuleFor(config => config["SQL:LoginData"])
            .NotEmpty().WithMessage("SQL login data is empty");

        RuleFor(config => config["WebApiUrl"])
            .NotEmpty().WithMessage("WebApiUrl is empty");

        RuleFor(config => config["ReactAppUrl"])
            .NotEmpty().WithMessage("ReactAppUrl is empty");

        RuleFor(config => config["ConnectionStrings:MySqlContextConnectionString"])
            .NotEmpty().WithMessage("Context connection string is empty");

        RuleFor(config => config["ConnectionStrings:MySqlIdentityContextConnectionString"])
            .NotEmpty().WithMessage("Identity context connection string is empty");

        RuleFor(config => config["ConnectionStrings:MySqlSerilogConnectionString"])
            .NotEmpty().WithMessage("Serilog connection string is empty");

        RuleFor(config => config["Email:Password"])
            .NotEmpty().WithMessage("Email login data is empty");

        RuleFor(config => config["Email:Address"])
            .NotEmpty().WithMessage("Email address is empty");

        RuleFor(config => config["Email:Username"])
            .NotEmpty().WithMessage("Email username is empty");

        RuleFor(config => config["JwtSettings:SecretKey"])
            .NotEmpty().WithMessage("JwtSettingsSecretKey is empty");

        RuleFor(config => config["Identity:UserPassword"])
            .NotEmpty().WithMessage("IdentityUserPassword is empty");

        RuleFor(config => config["Identity:AdminPassword"])
            .NotEmpty().WithMessage("IdentityAdminPassword is empty");

        RuleFor(config => config["Movies:ServiceApiKey"])
            .NotEmpty().WithMessage("MoviesServiceApiKey is empty");
    }
}