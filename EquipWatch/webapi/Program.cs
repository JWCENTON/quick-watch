using DAL.Equipment;
using DAL.Equipment.Database;
using DAL.Equipment.InMemory;
using webapi.Equipment.Services;
using webapi.Models.Equipment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IEquipmentDao, InMemoryEquipmentDao>();
}
else
{
    builder.Services.AddSingleton<IEquipmentDao, DatabaseEquipmentDao>();
}

builder.Services.AddTransient<IEquipmentService, EquipmentService>();

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
