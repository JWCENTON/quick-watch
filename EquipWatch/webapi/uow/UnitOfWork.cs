using DAL;
using webapi.Entities.ClientApi.Services;
using webapi.Entities.CompanyApi.Services;
using webapi.Entities.EquipmentApi.Services;

namespace webapi.uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IClientService _clientService;
        private ICompanyService _companyService;
        private IEquipmentService _equipmentService;
        

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IClientService Clients => _clientService ??= new ClientService(_context);

        public ICompanyService Companies => _companyService ??= new CompanyService(_context);

        public IEquipmentService Equipments => _equipmentService ??= new EquipmentService(_context);

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
