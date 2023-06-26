using DAL;
using Domain.Equipment.Models;
using Microsoft.EntityFrameworkCore;

namespace webapi.Entities.EquipmentApi.Services;

public class EquipmentService : IEquipmentService
{
    private readonly DatabaseContext _context;


    public EquipmentService(DatabaseContext context)
    {
        _context = context;
        if (!_context.Equipment.Any())
        {
            SeedEquipment();
        }
    }
    private async void SeedEquipment()
    {
        //Guid.NewGuid(), "123", "Fog machine", "my house", 4, false
        //await Create(new Equipment()
        //{
        //    Id = Guid.NewGuid(),
        //    SerialNumber = "123", 
        //    Category = "Fog machine", 
        //    Location = "my house", 
        //    Condition = 4, 
        //    IsCheckedOut = false
        //});
        await Create(new Equipment(Guid.NewGuid(), "133", "Fog machine", "my house", 3, false));
        await Create(new Equipment(Guid.NewGuid(), "14235", "Fog machine", "my house", 5, true));
        await Create(new Equipment(Guid.NewGuid(), "11234", "Fog machine", "my house", 1, false));
    }

    public async Task<List<Equipment>> GetAll()
    {
        return await _context.Equipment.ToListAsync();
    }

    public async Task<Equipment> Get(Guid id)
    {
        return await _context.Equipment.FirstAsync(e => e.Id == id);
    }

    public async Task Create(Equipment entity)
    {
        await _context.Equipment.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Equipment entity)
    {
        throw new NotImplementedException();
    }

    public async Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }


}