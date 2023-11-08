namespace DAL.Repositories.BookedEquipment;

public interface IBookedEquipmentRepository : IBaseRepository<Domain.BookedEquipment.Models.BookedEquipment>
{
    public Task<List<Domain.Equipment.Models.Equipment>> GetCommissionEquipmentAsync(Guid commissionId);
    public Task<Domain.BookedEquipment.Models.BookedEquipment> GetCurrentBookForEquipmentAsync(Guid equipmentId);
    public Task<Domain.BookedEquipment.Models.BookedEquipment> GetCurrentBookForEquipmentWithDetailsAsync(Guid equipmentId);
}