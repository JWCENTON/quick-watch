using DAL;
using Domain.User.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using webapi;
using webapi.Extensions;
using webapi.Middleware;
using webapi.Services;
using webapi.Validators;

var builder = WebApplication.CreateBuilder(args);
var environmentName = builder.Environment.EnvironmentName;

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .Build();

// while enabled instead of local config app will use the manually fetched one from Azure KeyVault also using Azure Database
//var azureConfig = new AzureDatabaseAndKeyVaultScript(configuration);
//azureConfig.OverrideConfigToTestAzureIntegration();

var configValidator = new AppConfigValidator();

var result = configValidator.Validate(configuration);
if (!result.IsValid)
{
    throw new KeyNotFoundException(result.Errors.First().ErrorMessage);
}

var connectionStringService = new ConnectionStringService(configuration, "SQL:LoginData");

var mySqlConnectionString = connectionStringService.GetConnectionString("MySqlContextConnectionString");
var mySqlIdentityConnectionString = connectionStringService.GetConnectionString("MySqlIdentityContextConnectionString");
connectionStringService.UpdateSerilogConnectionString("MySqlSerilogConnectionString");

builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)));
builder.Services.AddDbContext<IdentityContext>(options => options.UseMySql(mySqlIdentityConnectionString, ServerVersion.AutoDetect(mySqlIdentityConnectionString)));

builder.Services.Configure<EmailContext>(configuration.GetSection("Email"));

// required to test Email Services
builder.Services.AddSingleton<ISmtpClientWrapper>(provider =>
{
    var emailContext = configuration.GetSection("Email").Get<EmailContext>();
    return new SmtpClientWrapper(emailContext.Smtp, emailContext.Port, emailContext.Username, emailContext.Password);
});

// Set up Serilog logger and update serilog connection string with username and password

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "default", policy =>
    {
        policy.WithOrigins("https://localhost:7007",  "https://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[key: "JwtSettings:SecretKey"])),
        };
    });
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service Collection
builder.Services.AddMyDependencyGroup();

var app = builder.Build();

app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});


//configure exception handling middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting up the application");
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Application failed to start");
}
finally
{
    Log.Information("Shutting down the application");
    Log.CloseAndFlush();
}
