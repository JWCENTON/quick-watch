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
        return await _context.Equipment.ToListAsync();
    }

    public async Task<Domain.Equipment.Models.Equipment> GetAsync(Guid id)
    {
        return await _context.Equipment.FirstAsync(e => e.Id == id);
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
        throw new NotImplementedException();
    }


}