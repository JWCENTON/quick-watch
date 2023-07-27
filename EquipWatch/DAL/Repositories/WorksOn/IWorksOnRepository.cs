using Domain.User.Models;

namespace DAL.Repositories.WorksOn;

public interface IWorksOnRepository : IBaseRepository<Domain.WorksOn.Models.WorksOn>
{
    public Task<List<User>> GetCommissionEmployeesAsync(Guid commissionId);
}