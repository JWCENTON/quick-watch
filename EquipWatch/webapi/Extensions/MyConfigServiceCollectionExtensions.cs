using webapi.Entities.ClientApi.Services;
using webapi.Entities.CompanyApi.Services;
using webapi.Entities.EquipmentApi.Services;

namespace webapi.Extensions;

public static class MyConfigServiceCollectionExtensions
{
    public static IServiceCollection AddMyDependencyGroup(
        this IServiceCollection services)
    {
        services.AddTransient<IEquipmentService, EquipmentService>();
        services.AddTransient<ICompanyService, CompanyService>();
        services.AddTransient<IclientService, ClientService>();

        return services;
    }
}
