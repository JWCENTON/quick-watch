using System.Text;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain.User.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using webapi.Extensions;
using webapi.Middleware;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);
var environmentName = builder.Environment.EnvironmentName;
var configFileName = environmentName == "Development" ? "appsettings.Development.json" : "appsettings.json";

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile(configFileName, optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();

var loginDataKey = environmentName == "Development" ? "DatabaseLoginData" : "AzureDatabaseLoginData";

var loginData = configuration.GetSection("SQL")[loginDataKey].IsNullOrEmpty()
    ? throw new InvalidOperationException("MySql login string not found.")
    : configuration.GetSection("SQL")[loginDataKey];

var mySqlDatabase = configuration.GetConnectionString("MySqlContextConnection").IsNullOrEmpty()
    ? throw new InvalidOperationException("Connection string 'MySqlContextConnection' not found.")
    : configuration.GetConnectionString("MySqlContextConnection");

var mySqlIdentity = configuration.GetConnectionString("MySqlIdentityContextConnection").IsNullOrEmpty()
    ? throw new InvalidOperationException("Connection string 'MySqlIdentityContextConnection' not found.")
    : configuration.GetConnectionString("MySqlIdentityContextConnection");

var mySqlConnectionString = mySqlDatabase + loginData;
var mySqlIdentityConnectionString = mySqlIdentity + loginData;

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
if (configuration.GetSection("Serilog").GetSection("WriteTo:0:Args")["connectionString"].IsNullOrEmpty())
{
    throw new InvalidOperationException("Connection string for SeriLog not found.");
}
configuration.GetSection("Serilog").GetSection("WriteTo:0:Args")["connectionString"] += configuration.GetSection("SQL")[loginDataKey];

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

