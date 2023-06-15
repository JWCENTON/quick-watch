namespace DAL;

public interface IDao<T>
{
    List<T> GetAll();

    T Get(Guid id);

    void Create(T entity);

    void Update(T entity);

    void Remove(Guid id);
}