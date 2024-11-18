namespace WebShopSolution.Shared.Interfaces;

public interface IRepository<IEntity, Tid> where IEntity : IEntity<Tid>
{
    Task<IEnumerable<IEntity>> GetAllAsync();
    Task<IEntity> GetByIdAsync(Tid id);
    Task AddAsync(IEntity entity);
    Task DeleteAsync(Tid id);
}