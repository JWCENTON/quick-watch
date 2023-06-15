using Domain;
using webapi.Infrastructure.Repositories.Equipment;

namespace webapi.Infrastructure.uow
{
    public interface IUnitOfWork : IDisposable
    {
        IEquipmentRepository<Equipment> Equipments { get; }
        Task Save();
    }
}
