using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security.Principal;
using WebShopSolution.Shared.Interfaces;
namespace WebShopSolution.Sql.Repositories;

public class BaseRepository<IEntity, Tid, TContext> : IRepository<IEntity, Tid>
    where IEntity : class, IEntity<Tid>
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly DbSet<IEntity> _entities;

    public BaseRepository(TContext context)
    {
        _context = context;
        _entities = _context.Set<IEntity>();
    }
    public async Task<IEnumerable<IEntity>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }
    public async Task<IEntity> GetByIdAsync(Tid id)
    {
        return await _entities.FindAsync(id);
    }
    public async Task AddAsync(IEntity entity)
    {
         await _entities.AddAsync(entity);
    }
    public async Task DeleteAsync(Tid id)
    {
        var entity = await _entities.FindAsync(id);
        if (entity is not null)
            _entities.Remove(entity);
    }
}