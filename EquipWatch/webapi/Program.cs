
using System.Text;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain.User.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using webapi.Extensions;
using webapi.Middleware;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DatabaseContextConnection") ?? throw new InvalidOperationException("Connection string 'DatabaseContextConnection' not found.");
var identityConnectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

string mySqlConnectionString = "server=localhost;port=3306;database=EquipWatch;uid=root;pwd=";


builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)));
builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(identityConnectionString));

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Set up Serilog logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();



builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mySecretKeyhahahaha"))
        };
    });
// Add services to the container.

builder.Services.AddControllers()
    // Solves problem with cyclical dependency 
    .AddNewtonsoftJson(options => {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//    (c =>
//{
//    c.SwaggerGeneratorOptions.SwaggerDocs("v1", new OpenApiInfo
//    {
//        Title = "jwtToken_Auth_API",
//        Version = "v1"
//    });
//});

// Service Collection
builder.Services.AddMyDependencyGroup();

builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("https://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

