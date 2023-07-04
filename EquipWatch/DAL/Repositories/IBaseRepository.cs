namespace DAL.Repositories;

public interface IBaseRepository<T>
{
    Task<List<T>> GetAllAsync();

    Task<T> GetAsync(Guid id);

    Task CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task RemoveAsync(Guid id);
}