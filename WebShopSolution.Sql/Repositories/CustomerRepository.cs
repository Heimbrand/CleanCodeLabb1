using Microsoft.EntityFrameworkCore;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;

namespace WebShopSolution.Sql.Repositories;

public class CustomerRepository : BaseRepository<Customer, int, WebShopDbContext>, ICustomerRepository
{

    private readonly WebShopDbContext _context;
    private readonly DbSet<Customer> _entities;
    public CustomerRepository(WebShopDbContext context) : base(context)
    {
        _context = context;
        _entities = _context.Set<Customer>();
    }
    public async Task UpdateCustomer(Customer customer)
    {
         _entities.Update(customer);
    }
}