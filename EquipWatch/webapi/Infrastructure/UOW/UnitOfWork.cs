using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;
using webapi.Infrastructure.Repositories.Equipment;

namespace webapi.Infrastructure.uow
{
    public class UnitOfWork : IUnitOfWork
    {
       private readonly EquipmentDbContext _context;

       public UnitOfWork(EquipmentDbContext context)
       {
           _context = context;
       }

       public IEquipmentRepository<Equipment> Equipments => new EquipmentRepository<Equipment>(_context);
       public void Dispose()
       {
            _context.Dispose();
            GC.SuppressFinalize(true);
       }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
