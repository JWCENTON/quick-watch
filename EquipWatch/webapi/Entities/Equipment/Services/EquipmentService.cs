using DAL.Equipment;

namespace webapi.Entities.Equipment.Services;

public class EquipmentService : IEquipmentService
{

    private IEquipmentDao Dao { get; set; }

    public EquipmentService(IEquipmentDao equipmentDao)
    {
        Dao = equipmentDao;
    }

    public Domain.Equipment.Equipment GetEquipment(Guid equipmentId)
    {
        return Dao.Get(equipmentId);
    }

    public List<Domain.Equipment.Equipment> GetAllEquipment()
    {
        return Dao.GetAll();
    }
}