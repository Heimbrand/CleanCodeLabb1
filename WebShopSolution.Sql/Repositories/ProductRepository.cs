using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;

namespace WebShopSolution.Sql.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}