using WebShopSolution.Shared.Interfaces;
using WebShopSolution.Sql.Entities;

namespace WebShopSolution.Sql.InterfaceRepos;

public interface IOrderRepository : IRepository<Order, int>
{
    
}