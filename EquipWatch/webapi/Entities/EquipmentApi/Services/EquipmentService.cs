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
        DatabaseContext.IfDbEmptyAddNewItems(context);
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