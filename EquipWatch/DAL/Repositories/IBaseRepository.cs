namespace DAL.Repositories;

public interface IBaseRepository<T>
{
    Task<List<T>> GetAll();

    Task<T> Get(Guid id);

    Task Create(T entity);

    Task Update(T entity);

    Task Remove(Guid id);
}