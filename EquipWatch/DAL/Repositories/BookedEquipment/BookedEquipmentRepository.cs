using Domain.EquipmentInUse.Models;
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

    public async Task CreateAsync(Domain.BookedEquipment.Models.BookedEquipment entity)
    {
        if (_context.BookedEquipments.Any(b =>
                b.EquipmentInUse.EquipmentId == entity.EquipmentInUse.EquipmentId && b.CommissionId == entity.CommissionId && !b.IsFinished))
        {
            throw new ArgumentException("Equipment is already assigned to this commission");
        }
        await _context.BookedEquipments.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.BookedEquipment.Models.BookedEquipment entity)
    {
        _context.BookedEquipments.Update(entity);
        await _context.SaveChangesAsync();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Domain.Equipment.Models.Equipment>> GetCommissionEquipmentAsync(Guid commissionId)
    {
        var bookings = _context.BookedEquipments.Where(booking => booking.CommissionId == commissionId && !booking.IsFinished);
        var equipment = bookings.Select(booking => booking.EquipmentInUse.Equipment);
        return equipment.ToListAsync();
    }

    public async Task<Domain.BookedEquipment.Models.BookedEquipment> GetCurrentBookForEquipmentWithDetailsAsync(Guid equipmentId)
    {
        var book = await _context.BookedEquipments
            .Include(b => b.EquipmentInUse).ThenInclude(e => e.Equipment)
            .Include(b => b.Commission)
            .FirstOrDefaultAsync(
            book => 
                book.EquipmentInUse.EquipmentId == equipmentId && 
                book.IsFinished == false &&
                book.EquipmentInUse.CreationTime < DateTime.Now);

        return book!;
    }

    public async Task<Domain.BookedEquipment.Models.BookedEquipment> GetCurrentBookForEquipmentAsync(Guid equipmentId)
    {
        var book = await _context.BookedEquipments.FirstOrDefaultAsync(
                book =>
                    book.EquipmentInUse.EquipmentId == equipmentId &&
                    book.IsFinished == false &&
                    book.EquipmentInUse.CreationTime < DateTime.Now);

        return book!;
    }
}