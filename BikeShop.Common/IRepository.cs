namespace BikeShop.Common;

public interface IRepository<T> where T : class, IEntity
{
    Task<T?> GetByIdAsync(Guid id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> GetAllAsync(int page, int amount);

    Task AddAsync(T entity);

    Task Update(T entity);

    Task Delete(T entity);

    Task SaveChangesAsync();
}