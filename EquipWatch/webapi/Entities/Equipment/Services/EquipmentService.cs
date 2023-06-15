using DAL.Equipment;
using DAL.Equipment.Database;
using webapi.Models.Equipment.Services;

namespace webapi.Equipment.Services;

public class EquipmentService : IEquipmentService
{

    private IEquipmentDao Dao { get; set; }

    public EquipmentService()
    {
        Dao = new DatabaseEquipmentDao();
    }

    public Domain.Equipment.Equipment GetEquipment(Guid equipmentId)
    {
        return Dao.Get(equipmentId);
    }

    public List<Domain.Equipment.Equipment> GetAllEquipment()
    {
        throw new NotImplementedException();
    }
}