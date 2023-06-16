using DAL.Equipment;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Company;
using DAL.Company.InMemoryAccess;
using Domain.User.Models;
using webapi.Entities.Equipment.Services;
using DAL.Equipment.InMemoryAccess;
using DAL.Equipment.DatabaseAccess;
using webapi.Entities.Company.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DatabaseContextConnection") ?? throw new InvalidOperationException("Connection string 'DatabaseContextConnection' not found.");

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DatabaseContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IEquipmentDao, InMemoryEquipmentDao>();
    builder.Services.AddSingleton<ICompanyDao, InMemoryCompanyDao>();
}
else
{
    builder.Services.AddSingleton<IEquipmentDao, DatabaseEquipmentDao>();
}

builder.Services.AddTransient<IEquipmentService, EquipmentService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
