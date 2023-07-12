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
using DAL.Repositories.User;
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
        private IUserRepository _userService;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IBookedEquipmentRepository BookedEquipment => _bookedEquipmentService ??= new BookedEquipmentRepository(_context);

        public ICheckInRepository CheckIns => _checkInService ??= new CheckInRepository(_context);

        public ICheckOutRepository CheckOuts => _checkOutService ??= new CheckOutRepository(_context);

        public IClientRepository Clients => _clientService ??= new ClientRepository(_context);

        public ICommissionRepository Commissions => _commissionService ??= new CommissionRepository(_context);

        public ICompanyRepository Companies => _companyService ??= new CompanyRepository(_context);

        public IEmployeeRepository Employees => _employeeService ??= new EmployeeRepository(_context);

        public IEquipmentRepository Equipments => _equipmentService ??= new EquipmentRepository(_context);

        public IInviteRepository Invites => _inviteService ??= new InviteRepository(_context);

        public IWorksOnRepository WorksOn => _worksOnService ??= new WorksOnRepository(_context);

        public IUserRepository Users => _userService ??= new UserRepository(_context);


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
