using DAL;
using DAL.Repositories.BookedEquipment;
using DAL.Repositories.CheckIn;
using DAL.Repositories.CheckOut;
using DAL.Repositories.Client;
using DAL.Repositories.Commission;
using DAL.Repositories.Company;
using DAL.Repositories.Employee;
using DAL.Repositories.Equipment;
using DAL.Repositories.EquipmentInUse;
using DAL.Repositories.Invite;
using DAL.Repositories.Reservation;
using DAL.Repositories.User;
using DAL.Repositories.WorksOn;
using Domain.User.Models;
using Microsoft.AspNetCore.Identity;

namespace webapi.uow;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IdentityContext _identityContext;
    private IBookedEquipmentRepository _bookedEquipmentRepository;
    private ICheckInRepository _checkInRepository;
    private ICheckOutRepository _checkOutRepository;
    private IClientRepository _clientRepository;
    private ICommissionRepository _commissionRepository;
    private ICompanyRepository _companyRepository;
    private IEmployeeRepository _employeeRepository;
    private IEquipmentRepository _equipmentRepository;
    private IInviteRepository _inviteRepository;
    private IWorksOnRepository _worksOnRepository;
    private IReservationRepository _reservationRepository;
    private IEquipmentInUseRepository _equipmentInUseRepository;
    private IUserRepository _userRepository;

    public UnitOfWork(DatabaseContext context, IdentityContext identityContext, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _context = context;
        _identityContext = identityContext;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IBookedEquipmentRepository BookedEquipment => _bookedEquipmentRepository ??= new BookedEquipmentRepository(_context, _identityContext);

    public ICheckInRepository CheckIns => _checkInRepository ??= new CheckInRepository(_context, _identityContext);

    public ICheckOutRepository CheckOuts => _checkOutRepository ??= new CheckOutRepository(_context, _identityContext);

    public IClientRepository Clients => _clientRepository ??= new ClientRepository(_context, _identityContext);

    public ICommissionRepository Commissions => _commissionRepository ??= new CommissionRepository(_context, _identityContext);

    public ICompanyRepository Companies => _companyRepository ??= new CompanyRepository(_context, _identityContext);

    public IEmployeeRepository Employees => _employeeRepository ??= new EmployeeRepository(_context, _identityContext);

    public IEquipmentRepository Equipments => _equipmentRepository ??= new EquipmentRepository(_context, _identityContext);

    public IInviteRepository Invites => _inviteRepository ??= new InviteRepository(_context, _identityContext);

    public IWorksOnRepository WorksOn => _worksOnRepository ??= new WorksOnRepository(_context, _identityContext);

    public IEquipmentInUseRepository EquipmentInUse => _equipmentInUseRepository ??= new EquipmentInUseRepository(_context, _identityContext);

    public IReservationRepository Reservation => _reservationRepository ??= new ReservationRepository(_context, _identityContext);

    public IUserRepository User => _userRepository ??= new UserRepository(_userManager, _signInManager);

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}