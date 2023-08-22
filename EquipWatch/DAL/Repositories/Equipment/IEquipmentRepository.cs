using DAL.Repositories;

namespace DAL.Repositories.Equipment;

public interface IEquipmentRepository : IBaseRepository<Domain.Equipment.Models.Equipment>
{
    public Task<List<Domain.Equipment.Models.Equipment>> GetAllAvailableAsync();
}