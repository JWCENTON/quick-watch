using DAL.Repositories.Reservation;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.EquipmentInUse;

public class EquipmentInUseRepository : IEquipmentInUseRepository
{
    private readonly DatabaseContext _context;

    public EquipmentInUseRepository(DatabaseContext context, IdentityContext identityContext)
    {
        _context = context;
        Seed.IfDbEmptyAddNewItems(context, identityContext);
    }

    public async Task<List<Domain.EquipmentInUse.Models.EquipmentInUse>> GetAllAsync()
    {
        return await _context.EquipmentInUse.ToListAsync();
    }

    public async Task<Domain.EquipmentInUse.Models.EquipmentInUse> GetAsync(Guid id)
    {
        var equipmentInUse = await _context.EquipmentInUse.FirstOrDefaultAsync(c => c.Id == id);
        return equipmentInUse ?? throw new KeyNotFoundException("Equipment in use with given Id was not found");
    }

    public async Task CreateAsync(Domain.EquipmentInUse.Models.EquipmentInUse entity)
    {
        await _context.EquipmentInUse.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.EquipmentInUse.Models.EquipmentInUse entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
