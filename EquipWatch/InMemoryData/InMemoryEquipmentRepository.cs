using DAL.Equipment;

namespace webapi.Equipment.Repository;

public class InMemoryEquipmentRepository : IEquipmentRepository
{

    private HashSet<Domain.Equipment.Equipment> _equipment;

    private static int _equipmentCounter = 0;

    public InMemoryEquipmentRepository()
    {
        SeedEquipment();
    }
    private void SeedEquipment()
    {
        throw new NotImplementedException();
    }

    public List<Domain.Equipment.Equipment> GetAll()
    {
        throw new NotImplementedException();
    }

    public Domain.Equipment.Equipment Get(int id)
    {
        throw new NotImplementedException();
    }

    public void Create(Domain.Equipment.Equipment entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Domain.Equipment.Equipment entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }
}