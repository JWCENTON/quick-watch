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
using Microsoft.EntityFrameworkCore;

namespace webapi.uow
{
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
        IUserRepository Users { get; } // probably to be removed
        // Add other repositories as needed

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
