using DAL.Repositories.Client;
using DAL.Repositories.Company;
using DAL.Repositories.Equipment;
using DTO.OldMappers;
using webapi.uow;


namespace webapi.Extensions;

public static class MyConfigServiceCollectionExtensions
{
    public static IServiceCollection AddMyDependencyGroup(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IEquipmentRepository, EquipmentRepository>();
        services.AddTransient<ICompanyRepository, CompanyRepository>();
        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddAutoMapper(typeof(CreateMappingProfile));
        services.AddAutoMapper(typeof(UpdateMappingProfile));
        services.AddAutoMapper(typeof(FullMappingProfile));
        return services;
    }
}
