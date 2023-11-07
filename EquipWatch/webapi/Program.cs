using Serilog;
using webapi.Extensions;
using webapi.Middleware;
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

// Validate configuration
var configValidator = new AppConfigValidator();
var result = configValidator.Validate(configuration);

if (!result.IsValid)
{
    throw new KeyNotFoundException(result.Errors.First().ErrorMessage);
}

// Set up Serilog logger and configure the logger

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

// Clear the default logging providers and add Serilog
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "default", policy =>
    {
        policy.WithOrigins(configuration["WebApiUrl"], configuration["ReactAppUrl"])
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services using extension method
builder.Services.AddMyServices(configuration);

var app = builder.Build();

app.UseDeveloperExceptionPage();

// Configure the Swagger UI.
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

// Mao the controllers
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
