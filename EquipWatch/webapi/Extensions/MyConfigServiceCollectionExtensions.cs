using System.Configuration;
using System.Reflection;
using DAL;
using DAL.Repositories.BookedEquipment;
using DAL.Repositories.CheckIn;
using DAL.Repositories.CheckOut;
using DAL.Repositories.Client;
using DAL.Repositories.Commission;
using DAL.Repositories.Company;
using DAL.Repositories.Employee;
using DAL.Repositories.Equipment;
using DAL.Repositories.Invite;
using DAL.Repositories.WorksOn;
using DTO.Mappers;
using DTO.Validators;
using webapi.Services;
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

        services.AddScoped<EquipmentDTOValidator>();
        services.AddScoped<CompanyDTOValidator>();

        services.AddTransient<IEmailService, EmailService>();

        services.AddAutoMapper(Assembly.Load("DTO"));
        return services;
    }
}
