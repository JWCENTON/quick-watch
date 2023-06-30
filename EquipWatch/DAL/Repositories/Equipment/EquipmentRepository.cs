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

    public async Task<List<Domain.Equipment.Models.Equipment>> GetAll()
    {
        return await _context.Equipment.ToListAsync();
    }

    public async Task<Domain.Equipment.Models.Equipment> Get(Guid id)
    {
        return await _context.Equipment.FirstAsync(e => e.Id == id);
    }

    public async Task Create(Domain.Equipment.Models.Equipment entity)
    {
        await _context.Equipment.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Domain.Equipment.Models.Equipment entity)
    {
        throw new NotImplementedException();
    }

    public async Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }


}