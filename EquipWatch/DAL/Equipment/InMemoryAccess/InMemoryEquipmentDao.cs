namespace DAL.Equipment.InMemoryAccess;

public class InMemoryEquipmentDao : IEquipmentDao
{

    private HashSet<Domain.Equipment.Equipment> _equipment;

    public InMemoryEquipmentDao()
    {
        _equipment = new HashSet<Domain.Equipment.Equipment>();
        SeedEquipment();
    }
    private void SeedEquipment()
    {
        Create(new Domain.Equipment.Equipment(Guid.NewGuid(), "123", "Fog machine", "my house", 4, false));
        Create(new Domain.Equipment.Equipment(Guid.NewGuid(), "133", "Fog machine", "my house", 3, false));
        Create(new Domain.Equipment.Equipment(Guid.NewGuid(), "14235", "Fog machine", "my house", 5, true));
        Create(new Domain.Equipment.Equipment(Guid.NewGuid(), "11234", "Fog machine", "my house", 1, false));
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