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
        Create(new Domain.Equipment.Equipment());
    }

    public List<Domain.Equipment.Equipment> GetAll()
    {
        return _equipment.ToList();
    }

    public Domain.Equipment.Equipment Get(Guid id)
    {
        return _equipment.First(e => e.Id == id);
    }

    public void Create(Domain.Equipment.Equipment entity)
    {
        _equipment.Add(entity);
        _equipmentCounter++;
    }

    public void Update(Domain.Equipment.Equipment entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(Guid id)
    {
        throw new NotImplementedException();
    }
}