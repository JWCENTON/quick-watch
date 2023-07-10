using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.BookedEquipment;

public class BookedEquipmentRepository : IBookedEquipmentRepository
{
    private readonly DatabaseContext _context;

    public BookedEquipmentRepository(DatabaseContext context)
    {
        _context = context;
        DatabaseContext.IfDbEmptyAddNewItems(context);
    }

    public async Task<List<Domain.BookedEquipment.Models.BookedEquipment>> GetAllAsync()
    {
        return await _context.BookedEquipments.ToListAsync();
    }
}