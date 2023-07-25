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
}