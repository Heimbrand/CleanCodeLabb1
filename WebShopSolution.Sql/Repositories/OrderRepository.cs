using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;

namespace WebShopSolution.Sql.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<IEnumerable<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}