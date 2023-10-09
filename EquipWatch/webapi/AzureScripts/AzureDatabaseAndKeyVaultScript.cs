using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.IdentityModel.Tokens;

namespace webapi;

public class AzureDatabaseAndKeyVaultScript
{
    private readonly IConfiguration _configuration;
    private readonly SecretClient _secretClient;

    public AzureDatabaseAndKeyVaultScript(IConfiguration configuration)
    {
        _configuration = configuration;
        var secretClientService = new SecretClientService(configuration);
        _secretClient = secretClientService.CreateSecretClient();
    }

    public void OverrideConfigToTestAzureIntegration()
    {
        // saved as variables to make easier debug
        var loginData = GetLoginData("SQLLoginData");
        ConfigureLocalSecrets();
        var contextConnectionString = GetConnectionString("MySqlContextConnectionString");
        var identityContextConnectionString = GetConnectionString("MySqlIdentityContextConnectionString");
        var serilogContextConnectionString = UpdateSerilogConnectionString();
    }

    private string GetLoginData(string key)
    {
        var secret = _secretClient.GetSecret(key).Value;

        var loginData = secret.Value.IsNullOrEmpty()
            ? throw new KeyNotFoundException("Azure MySQL login data is empty")
            : secret.Value;
        _configuration["SQL:LoginData"] = loginData;
        return loginData;
    }

    private void ConfigureLocalSecrets()
    {
        var secret = _secretClient.GetSecret("EmailPassword").Value;

        var password = secret.Value.IsNullOrEmpty()
            ? throw new KeyNotFoundException("Azure Email password is empty")
            : secret.Value;

        _configuration.GetSection("Email")["Password"] = password;

        secret = _secretClient.GetSecret("EmailAddress").Value;

        var address = secret.Value.IsNullOrEmpty()
            ? throw new KeyNotFoundException("Azure Email address is empty")
            : secret.Value;

        _configuration.GetSection("Email")["Username"] = address;
        _configuration.GetSection("Email")["Address"] = address;

        secret = _secretClient.GetSecret("JwtSettingsSecretKey").Value;

        var secretKey = secret.Value.IsNullOrEmpty()
            ? throw new KeyNotFoundException("Azure JwtSettingsSecretKey is empty")
            : secret.Value;

        _configuration["JwtSettings:SecretKey"] = secretKey;

        secret = _secretClient.GetSecret("IdentityUserPassword").Value;

        var identityUserPassword = secret.Value.IsNullOrEmpty()
            ? throw new KeyNotFoundException("Azure IdentityUserPassword is empty")
            : secret.Value;

        _configuration["Identity:UserPassword"] = identityUserPassword;

        secret = _secretClient.GetSecret("IdentityAdminPassword").Value;

        var identityAdminPassword = secret.Value.IsNullOrEmpty()
            ? throw new KeyNotFoundException("Azure IdentityAdminPassword is empty")
            : secret.Value;

        _configuration["Identity:AdminPassword"] = identityAdminPassword;

        secret = _secretClient.GetSecret("MoviesServiceApiKey").Value;

        var moviesServiceApiKey = secret.Value.IsNullOrEmpty()
            ? throw new KeyNotFoundException("Azure MoviesServiceApiKey is empty")
            : secret.Value;

        _configuration["Movies:ServiceApiKey"] = moviesServiceApiKey;
    }


    private string GetConnectionString(string key)
    {
        var connectionString = GetSecretValue(key);
        _configuration[$"ConnectionStrings:{key}"] = connectionString;
        return connectionString;
    }

    private string UpdateSerilogConnectionString()
    {
        var mysqlItem = _configuration.GetSection("Serilog:WriteTo")
                            .GetChildren()
                            .FirstOrDefault(item => string.Equals(item["Name"], "MySQL", StringComparison.OrdinalIgnoreCase))
                        ?? throw new KeyNotFoundException("Serilog WriteTo MySql element not found.");
        mysqlItem["Args:connectionString"] = GetConnectionString("MySqlSerilogConnectionString"); ;
        return mysqlItem["Args:connectionString"];
    }

    private string GetSecretValue(string secretName)
    {
        var secret = _secretClient.GetSecret(secretName).Value;
        var connectionString = secret.Value.IsNullOrEmpty()
            ? throw new InvalidOperationException($"{secretName} value is empty.")
            : secret.Value;

        return connectionString;
    }
}
public class SecretClientService
{
    private readonly IConfiguration _configuration;

    public SecretClientService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SecretClient CreateSecretClient()
    {
        var keyVaultName = _configuration["TestKeyVaultName"].IsNullOrEmpty()
            ? throw new InvalidOperationException("TestKeyVaultName string not found.")
            : _configuration["TestKeyVaultName"];

        var kvUri = $"https://{keyVaultName}.vault.azure.net";

        return new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
    }
}