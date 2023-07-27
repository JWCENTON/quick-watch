using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.BookedEquipment;

public interface IBookedEquipmentRepository : IBaseRepository<Domain.BookedEquipment.Models.BookedEquipment>
{
    public Task<List<Domain.Equipment.Models.Equipment>> GetCommissionEquipmentAsync(Guid commissionId);
}