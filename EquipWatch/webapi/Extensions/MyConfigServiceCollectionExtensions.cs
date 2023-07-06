using System.Reflection;
using DAL.Repositories.Client;
using DAL.Repositories.Company;
using DAL.Repositories.Equipment;
using DTO.Mappers;
using DTO.Validators;
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
        services.AddScoped<EquipmentDTOValidator>();

        //both works
        services.AddAutoMapper(typeof(EquipmentMappingProfile));
        services.AddAutoMapper(typeof(CheckInMappingProfile));
        services.AddAutoMapper(typeof(CheckOutMappingProfile));
        services.AddAutoMapper(typeof(ClientMappingProfile));
        services.AddAutoMapper(typeof(CommissionMappingProfile));
        services.AddAutoMapper(typeof(CompanyMappingProfile));
        services.AddAutoMapper(typeof(EmployMappingProfile));
        services.AddAutoMapper(typeof(InviteMappingProfile));
        //services.AddAutoMapper(Assembly.Load("DTO"));
        return services;
    }
}
