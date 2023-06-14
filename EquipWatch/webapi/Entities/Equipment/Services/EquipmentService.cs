using DAL.Equipment;
using DAL.Equipment.Database;
using DAL.Equipment.InMemory;
using webapi.Models.Equipment.Services;

namespace webapi.Equipment.Services;

public class EquipmentService : IEquipmentService
{

    private IEquipmentRepository _repository { get; set; }

    public EquipmentService()
    {
        _repository = new DatabaseEquipmentRepository();
    }

    public Domain.Equipment.Equipment GetEquipment(string equipmentId)
    {
        return _repository.Get();
    }
}