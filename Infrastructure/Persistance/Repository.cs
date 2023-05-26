using N5NowApi.Domain.Primitives;
using N5NowApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;
public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task Update(T entity)
    {
        var exists = await _dbSet.FindAsync(entity.Id);

        if (exists is not null)
        {
            _dbSet.Update(entity);
        }
    }

    public async Task Delete(T entity)
    {
        var exists = await _dbSet.FirstOrDefaultAsync(f => f.Id == entity.Id);

        if (exists is not null)
        {
            _dbSet.Remove(entity);
        }
    }
}
