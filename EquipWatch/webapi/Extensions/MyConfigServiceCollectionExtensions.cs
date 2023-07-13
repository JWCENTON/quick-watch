using System.Reflection;
using DAL.Repositories.BookedEquipment;
using DAL.Repositories.CheckIn;
using DAL.Repositories.CheckOut;
using DAL.Repositories.Client;
using DAL.Repositories.Commission;
using DAL.Repositories.Company;
using DAL.Repositories.Employee;
using DAL.Repositories.Equipment;
using DAL.Repositories.Invite;
using DAL.Repositories.User;
using DAL.Repositories.WorksOn;
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
        services.AddTransient<IBookedEquipmentRepository, BookedEquipmentRepository>();
        services.AddTransient<ICheckInRepository, CheckInRepository>();
        services.AddTransient<ICheckOutRepository, CheckOutRepository>();
        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddTransient<ICommissionRepository, CommissionRepository>();
        services.AddTransient<ICompanyRepository, CompanyRepository>();
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        services.AddTransient<IEquipmentRepository, EquipmentRepository>();
        services.AddTransient<IInviteRepository, InviteRepository>();
        services.AddTransient<IWorksOnRepository, WorksOnRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddScoped<EquipmentDTOValidator>();
        services.AddScoped<CompanyDTOValidator>();

        //both works
        services.AddAutoMapper(typeof(EquipmentMappingProfile));
        services.AddAutoMapper(typeof(CheckInMappingProfile));
        services.AddAutoMapper(typeof(CheckOutMappingProfile));
        services.AddAutoMapper(typeof(ClientMappingProfile));
        services.AddAutoMapper(typeof(CommissionMappingProfile));
        services.AddAutoMapper(typeof(CompanyMappingProfile));
        services.AddAutoMapper(typeof(EmployMappingProfile));
        services.AddAutoMapper(typeof(InviteMappingProfile));
        services.AddAutoMapper(typeof(UserMappingProfile));

        //services.AddAutoMapper(Assembly.Load("DTO"));
        return services;
    }
}
