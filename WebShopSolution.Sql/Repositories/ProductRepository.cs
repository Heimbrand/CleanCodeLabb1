using Microsoft.EntityFrameworkCore;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;

namespace WebShopSolution.Sql.Repositories;

public class ProductRepository : BaseRepository<Product, int, WebShopDbContext>, IProductRepository
{
    private readonly WebShopDbContext _context;
    private readonly DbSet<Product> _entities;
    public ProductRepository(WebShopDbContext context) : base(context)
    {
        _context = context;
        _entities = _context.Set<Product>();
    }
    public async Task UpdateProduct(Product product)
    {
        _entities.Update(product);
    }
}