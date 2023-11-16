using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Reservation;

public class ReservationRepository : IReservationRepository
{
    private readonly DatabaseContext _context;

    public ReservationRepository(DatabaseContext context, IdentityContext identityContext)
    {
        _context = context;
        Seed.IfDbEmptyAddNewItems(context, identityContext);
    }

    public async Task<List<Domain.Reservation.Models.Reservation>> GetAllAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    public async Task<Domain.Reservation.Models.Reservation> GetAsync(Guid id)
    {
        var reservation = await _context.Reservations.FirstOrDefaultAsync(c => c.Id == id);
        return reservation ?? throw new KeyNotFoundException("Reservation With given Id was not found");
    }

    public async Task CreateAsync(Domain.Reservation.Models.Reservation entity)
    {
        await _context.Reservations.AddAsync(entity);
    }

    public async Task UpdateAsync(Domain.Reservation.Models.Reservation entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}