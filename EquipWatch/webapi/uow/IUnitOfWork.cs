using webapi.Entities.ClientApi.Services;
using webapi.Entities.CompanyApi.Services;
using webapi.Entities.EquipmentApi.Services;

namespace webapi.uow
{
    public interface IUnitOfWork
    {
        IClientService Clients { get; }
        ICompanyService Companies { get; }
        IEquipmentService Equipments { get; }
        // Add other repositories as needed

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
