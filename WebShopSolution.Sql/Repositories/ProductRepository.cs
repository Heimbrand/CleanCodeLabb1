using Microsoft.EntityFrameworkCore;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;

namespace WebShopSolution.Sql.Repositories;

public class ProductRepository : BaseRepository<Product, int, WebShopDbContext>, IProductRepository
{
    public ProductRepository(WebShopDbContext context) : base(context) { }
    public Task UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}