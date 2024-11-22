using Microsoft.EntityFrameworkCore;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;
using WebShopSolution.Sql.Repositories.BaseRepoTests;

namespace WebShopSolution.Sql.Repositories;

public class Repository : BaseRepository<Product, int, WebShopDbContext>, IProductRepository
{
    private readonly WebShopDbContext _context;
    private readonly DbSet<Product> _entities;
    public Repository(WebShopDbContext context) : base(context)
    {
        _context = context;
        _entities = _context.Set<Product>();
    }
    public async Task UpdateProduct(Product product)
    {
        _entities.Update(product);
    }
}