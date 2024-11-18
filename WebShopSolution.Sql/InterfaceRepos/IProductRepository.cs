using WebShopSolution.Shared.Interfaces;
using WebShopSolution.Sql.Entities;

namespace WebShopSolution.Sql.InterfaceRepos;

public interface IProductRepository : IRepository<Product, int>
{
    
}