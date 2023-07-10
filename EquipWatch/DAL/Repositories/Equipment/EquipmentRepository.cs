using DAL;
using Domain.Equipment.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Equipment;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly DatabaseContext _context;


    public EquipmentRepository(DatabaseContext context)
    {
        _context = context;
        DatabaseContext.IfDbEmptyAddNewItems(context);
    }

    public async Task<List<Domain.Equipment.Models.Equipment>> GetAllAsync()
    {
        return await _context.Equipment.Include(equipment => equipment.Company).ToListAsync();
    }

    public async Task<Domain.Equipment.Models.Equipment> GetAsync(Guid id)
    {
        var equipment = await _context.Equipment.Include(equipment => equipment.Company).FirstOrDefaultAsync(e => e.Id == id);
        return equipment ?? throw new KeyNotFoundException("Equipment With given Id was not found");
    }

    public async Task CreateAsync(Domain.Equipment.Models.Equipment entity)
    {
        await _context.Equipment.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Equipment.Models.Equipment entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id)
    {
        var equipment = await _context.Equipment.Include(equipment => equipment.Company).FirstOrDefaultAsync(e => e.Id == id);
        if (equipment == null)
        {
            throw new KeyNotFoundException("Equipment With given Id was not found");
        }
        _context.Equipment.Remove(equipment);
        await _context.SaveChangesAsync();
    }


}