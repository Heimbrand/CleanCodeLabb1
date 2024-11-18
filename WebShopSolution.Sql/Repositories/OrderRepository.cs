using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;

namespace WebShopSolution.Sql.Repositories;

public class OrderRepository : BaseRepository<Order, int, WebShopDbContext>, IOrderRepository
{
    public OrderRepository(WebShopDbContext context) : base(context) { }
    public Task UpdateOrder(Order order)
    {
        throw new NotImplementedException();
    }
}