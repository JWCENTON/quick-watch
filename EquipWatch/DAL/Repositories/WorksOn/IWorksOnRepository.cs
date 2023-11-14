namespace DAL.Repositories.WorksOn;

public interface IWorksOnRepository : IBaseRepository<Domain.WorksOn.Models.WorksOn>
{
    public Task<List<Domain.User.Models.User>> GetCommissionAssignedEmployeesAsync(Guid commissionId);
    public Task<List<Domain.WorksOn.Models.WorksOn>> GeCurrentWorksOnByCommissionIdAsync(Guid commissionId);
}