using Microsoft.EntityFrameworkCore;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;
using WebShopSolution.Sql.Repositories.BaseRepoTests;

namespace WebShopSolution.Sql.Repositories;

public class OrderRepository : BaseRepository<Order, int, WebShopDbContext>, IOrderRepository
{
    private readonly WebShopDbContext _context;
    private readonly DbSet<Order> _entities;
    public OrderRepository(WebShopDbContext context) : base(context)
    {
        _context = context;
        _entities = _context.Set<Order>();
    }

    public async Task UpdateOrder(Order order)
    { 
        _entities.Update(order);
    }
}