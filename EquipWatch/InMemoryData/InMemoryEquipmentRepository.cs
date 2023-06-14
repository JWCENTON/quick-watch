namespace webapi.Equipment.Repository;

public class InMemoryEquipmentRepository : IEquipmentRepository
{

    private HashSet<Models.Equipment> _equipment;

    private static int _equipmentCounter = 0;

    public InMemoryEquipmentRepository()
    {
        SeedEquipment();
    }
    private void SeedEquipment()
    {
        throw new NotImplementedException();
    }

    public List<Models.Equipment> GetAll()
    {
        throw new NotImplementedException();
    }

    public Models.Equipment Get(int id)
    {
        throw new NotImplementedException();
    }

    public void Create(Models.Equipment entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Models.Equipment entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }
}