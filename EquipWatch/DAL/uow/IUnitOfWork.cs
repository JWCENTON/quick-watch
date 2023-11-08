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
using DAL.Repositories.WorksOn;

namespace webapi.uow;

public interface IUnitOfWork
{
    IBookedEquipmentRepository BookedEquipment { get; }
    ICheckInRepository CheckIns { get; }
    ICheckOutRepository CheckOuts { get; }
    IClientRepository Clients { get; }
    ICommissionRepository Commissions { get; }
    ICompanyRepository Companies { get; }
    IEmployeeRepository Employees { get; }
    IEquipmentRepository Equipments { get; }
    IInviteRepository Invites { get; }
    IWorksOnRepository WorksOn { get; }
    IReservationRepository Reservation { get; }
    IEquipmentInUseRepository EquipmentInUse { get; }
    // Add other repositories as needed

    void SaveChanges();
    Task SaveChangesAsync();
}