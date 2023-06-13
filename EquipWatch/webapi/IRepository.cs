namespace webapi;

public interface IRepository<T>
{
    List<T> GetAll();

    T Get(int id);

    void Create(T entity);

    void Update(T entity);

    void Remove(int id);
}