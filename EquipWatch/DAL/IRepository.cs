namespace webapi;

public interface IRepository<T>
{
    List<T> GetAll();

    T Get(Guid id);

    void Create(T entity);

    void Update(T entity);

    void Remove(Guid id);
}