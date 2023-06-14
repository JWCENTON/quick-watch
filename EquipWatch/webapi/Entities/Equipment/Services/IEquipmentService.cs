namespace webapi.Models.Equipment.Services;

public interface IEquipmentService
{
    public Domain.Equipment.Equipment GetEquipment(string equipmentId);
}