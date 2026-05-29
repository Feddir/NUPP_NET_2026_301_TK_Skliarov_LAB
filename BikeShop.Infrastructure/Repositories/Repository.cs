using BikeShop.Common;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    private readonly BikeShopContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(BikeShopContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(int page, int amount)
    {
        if (page <= 0)
        {
            page = 1;
        }

        if (amount <= 0)
        {
            amount = 10;
        }

        return await _dbSet
            .Skip((page - 1) * amount)
            .Take(amount)
            .ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task Update(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}