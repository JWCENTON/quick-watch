namespace webapi.Models.Equipment.Services;

public interface IEquipmentService
{
    public Domain.Equipment.Equipment GetEquipment(Guid equipmentId);
    public List<Domain.Equipment.Equipment> GetAllEquipment();

}