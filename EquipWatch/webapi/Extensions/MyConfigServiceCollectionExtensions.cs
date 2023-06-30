using webapi.Entities.ClientApi.Services;
using webapi.Entities.CompanyApi.Services;
using webapi.Entities.EquipmentApi.Services;
using webapi.uow;

namespace webapi.Extensions;

public static class MyConfigServiceCollectionExtensions
{
    public static IServiceCollection AddMyDependencyGroup(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IEquipmentService, EquipmentService>();
        services.AddTransient<ICompanyService, CompanyService>();
        services.AddTransient<IClientService, ClientService>();

        return services;
    }
}
