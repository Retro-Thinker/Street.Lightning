using System.Linq.Expressions;
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

    public async Task<T> GetByIdAsync(int id, params string[] propertyNames)
    {
        if (propertyNames.Length == 0)
        {
            return (await _context.Set<T>().FindAsync(id))!;
        }
        
        var entityType = _context.Model.FindEntityType(typeof(T));
        var key = entityType.FindPrimaryKey();

        var parameter = Expression.Parameter(typeof(T), "e");
        var body = key.Properties
            .Select(p => Expression.Equal(
                Expression.Property(parameter, p.Name),
                Expression.Constant(id)))
            .Aggregate((x, y) => Expression.AndAlso(x, y));

        var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);

        var query = _context.Set<T>().AsQueryable();

        foreach (var propertyName in propertyNames)
        {
            var navigation = entityType.GetNavigations().SingleOrDefault(n => n.Name == propertyName);
            if (navigation != null)
            {
                query = query.Include(navigation.Name);
            }
        }

        return await query.SingleOrDefaultAsync(predicate);
        
       // return await _context.Set<T>().FindAsync(id);
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