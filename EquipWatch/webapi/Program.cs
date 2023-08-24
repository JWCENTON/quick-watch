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
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();

var mySqlConnectionString = builder.Configuration.GetConnectionString("MySqlContextConnection") + configuration.GetSection("SQL")["LoginData"] ?? throw new InvalidOperationException("Connection string 'MySqlContextConnection' not found.");
var mySqlIdentityConnectionString = builder.Configuration.GetConnectionString("MySqlIdentityContextConnection") + configuration.GetSection("SQL")["LoginData"] ?? throw new InvalidOperationException("Connection string 'MySqlContextConnection' not found.");

builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)));
builder.Services.AddDbContext<IdentityContext>(options => options.UseMySql(mySqlIdentityConnectionString, ServerVersion.AutoDetect(mySqlIdentityConnectionString)));


builder.Services.Configure<EmailContext>(configuration.GetSection("Email"));

// required to test Email Services
builder.Services.AddSingleton<ISmtpClientWrapper>(provider =>
{
    var emailContext = configuration.GetSection("Email").Get<EmailContext>();
    return new SmtpClientWrapper(emailContext.Smtp, emailContext.Port, emailContext.Username, emailContext.Password);
});

// Set up Serilog logger
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

//configure exception handling middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseDeveloperExceptionPage();

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

