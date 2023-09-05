namespace webapi.Services;

public class ConnectionStringService
{
    private readonly IConfiguration _configuration;
    private readonly string? _loginData;

    public ConnectionStringService(IConfiguration configuration, string loginKey)
    {
        _configuration = configuration;
        _loginData = _configuration[loginKey];
    }

    public string GetConnectionString(string key)
    {
        return _configuration.GetConnectionString(key) + _loginData;
    }

    public void UpdateSerilogConnectionString(string key)
    {
        var mysqlItem = _configuration.GetSection("Serilog:WriteTo")
            .GetChildren()
            .FirstOrDefault(item => string.Equals(item["Name"], "MySQL", StringComparison.OrdinalIgnoreCase)) 
                        ?? throw new KeyNotFoundException("Serilog WriteTo MySql element not found.");
        mysqlItem["Args:connectionString"] = GetConnectionString(key);
    }
}