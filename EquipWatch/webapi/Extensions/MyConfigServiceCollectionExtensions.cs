using System.Reflection;
using System.Text;
using DAL;
using DAL.Repositories.BookedEquipment;
using DAL.Repositories.CheckIn;
using DAL.Repositories.CheckOut;
using DAL.Repositories.Client;
using DAL.Repositories.Commission;
using DAL.Repositories.Company;
using DAL.Repositories.Employee;
using DAL.Repositories.Equipment;
using DAL.Repositories.Invite;
using DAL.Repositories.User;
using DAL.Repositories.WorksOn;
using Domain.User.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webapi.Services;
using webapi.uow;


namespace webapi.Extensions;

public static class MyConfigServiceCollectionExtensions
{
    public static IServiceCollection AddMyServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringService = new ConnectionStringService(configuration, "SQL:LoginData");
        var mySqlConnectionString = connectionStringService.GetConnectionString("MySqlContextConnectionString");
        var mySqlIdentityConnectionString = connectionStringService.GetConnectionString("MySqlIdentityContextConnectionString");
        connectionStringService.UpdateSerilogConnectionString("MySqlSerilogConnectionString");

        services.AddDbContext<DatabaseContext>(options => options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)));
        services.AddDbContext<IdentityContext>(options => options.UseMySql(mySqlIdentityConnectionString, ServerVersion.AutoDetect(mySqlIdentityConnectionString)));

        // Email Configuration
        services.Configure<EmailContext>(configuration.GetSection("Email"));
        services.AddSingleton<ISmtpClientWrapper>(provider =>
        {
            var emailContext = configuration.GetSection("Email").Get<EmailContext>();
            return new SmtpClientWrapper(emailContext!.Smtp ?? string.Empty , emailContext!.Port, emailContext.UserName ?? string.Empty, emailContext.Password ?? string.Empty);
        });
        
        // Identity
        services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"] ?? string.Empty))
                };
            });

        // Logging
        services.AddLogging();

        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Register services from your MyDependencyGroup
        services.AddMyDependencyGroup();

        return services;
    }

    public static IServiceCollection AddMyDependencyGroup(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IBookedEquipmentRepository, BookedEquipmentRepository>();
        services.AddTransient<ICheckInRepository, CheckInRepository>();
        services.AddTransient<ICheckOutRepository, CheckOutRepository>();
        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddTransient<ICommissionRepository, CommissionRepository>();
        services.AddTransient<ICompanyRepository, CompanyRepository>();
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        services.AddTransient<IEquipmentRepository, EquipmentRepository>();
        services.AddTransient<IInviteRepository, InviteRepository>();
        services.AddTransient<IWorksOnRepository, WorksOnRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        //services.AddTransient<CompanyIdDTOValidator>();
        services.AddValidatorsFromAssembly(Assembly.Load("webapi"));

        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<IEmployeeServices, EmployeeServices>();

        services.AddAutoMapper(Assembly.Load("DTO"));

        return services;
    }
}
