using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.BookedEquipment;

public class BookedEquipmentRepository : IBookedEquipmentRepository
{
    private readonly DatabaseContext _context;

    public BookedEquipmentRepository(DatabaseContext context, IdentityContext identityContext)
    {
        _context = context;
        Seed.IfDbEmptyAddNewItems(context, identityContext);
    }

    public async Task<List<Domain.BookedEquipment.Models.BookedEquipment>> GetAllAsync()
    {
        return await _context.BookedEquipments.ToListAsync();
    }

    public Task<Domain.BookedEquipment.Models.BookedEquipment> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Domain.BookedEquipment.Models.BookedEquipment entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.BookedEquipment.Models.BookedEquipment entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Domain.Equipment.Models.Equipment>> GetCommissionEquipmentAsync(Guid commissionId)
    {
        var bookings = _context.BookedEquipments.Where(booking => booking.CommissionId == commissionId);
        var equipment = bookings.Select(booking => booking.Equipment);
        return equipment.ToListAsync();
    }
}