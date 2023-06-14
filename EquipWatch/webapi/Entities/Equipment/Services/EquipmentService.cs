using DAL.Equipment;
using DAL.Equipment.Database;
using DAL.Equipment.InMemory;
using webapi.Models.Equipment.Services;

namespace webapi.Equipment.Services;

public class EquipmentService : IEquipmentService
{

    private IEquipmentDao Dao { get; set; }

    public EquipmentService()
    {
        Dao = new DatabaseEquipmentDao();
    }

    public Domain.Equipment.Equipment GetEquipment(string equipmentId)
    {
        return Dao.Get();
    }
}