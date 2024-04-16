using Microsoft.EntityFrameworkCore;
using Street.Lightning.Application.Contracts.Persistence.Common;
using Street.Lightning.Persistence.DatabaseContext;

namespace Street.Lightning.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly StreetLightningDatabaseContext _context;

    public GenericRepository(StreetLightningDatabaseContext dbContext)
    {
        _context = dbContext;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}