using DAL;
using DAL.Repositories.BookedEquipment;
using DAL.Repositories.CheckIn;
using DAL.Repositories.CheckOut;
using DAL.Repositories.Client;
using DAL.Repositories.Commission;
using DAL.Repositories.Company;
using DAL.Repositories.Employee;
using DAL.Repositories.Equipment;
using DAL.Repositories.Invite;
using DAL.Repositories.WorksOn;

namespace webapi.uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IBookedEquipmentRepository _bookedEquipmentService;
        private ICheckInRepository _checkInService;
        private ICheckOutRepository _checkOutService;
        private IClientRepository _clientService;
        private ICommissionRepository _commissionService;
        private ICompanyRepository _companyService;
        private IEmployeeRepository _employeeService;
        private IEquipmentRepository _equipmentService;
        private IInviteRepository _inviteService;
        private IWorksOnRepository _worksOnService;
        private readonly IdentityContext _identityContext;

        public UnitOfWork(DatabaseContext context, IdentityContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        public IBookedEquipmentRepository BookedEquipment => _bookedEquipmentService ??= new BookedEquipmentRepository(_context, _identityContext);

        public ICheckInRepository CheckIns => _checkInService ??= new CheckInRepository(_context, _identityContext);

        public ICheckOutRepository CheckOuts => _checkOutService ??= new CheckOutRepository(_context, _identityContext);

        public IClientRepository Clients => _clientService ??= new ClientRepository(_context, _identityContext);

        public ICommissionRepository Commissions => _commissionService ??= new CommissionRepository(_context, _identityContext);

        public ICompanyRepository Companies => _companyService ??= new CompanyRepository(_context, _identityContext);

        public IEmployeeRepository Employees => _employeeService ??= new EmployeeRepository(_context, _identityContext);

        public IEquipmentRepository Equipments => _equipmentService ??= new EquipmentRepository(_context, _identityContext);

        public IInviteRepository Invites => _inviteService ??= new InviteRepository(_context, _identityContext);

        public IWorksOnRepository WorksOn => _worksOnService ??= new WorksOnRepository(_context, _identityContext);


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