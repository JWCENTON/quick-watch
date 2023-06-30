using DAL;
using DAL.Repositories.Client;
using DAL.Repositories.Company;
using DAL.Repositories.Equipment;

namespace webapi.uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IClientRepository _clientService;
        private ICompanyRepository _companyService;
        private IEquipmentRepository _equipmentService;
        

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IClientRepository Clients => _clientService ??= new ClientRepository(_context);

        public ICompanyRepository Companies => _companyService ??= new CompanyRepository(_context);

        public IEquipmentRepository Equipments => _equipmentService ??= new EquipmentRepository(_context);

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
