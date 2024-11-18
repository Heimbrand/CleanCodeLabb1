using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;

namespace WebShopSolution.Sql.Repositories;

public class CustomerRepository : BaseRepository<Customer, int, WebShopDbContext>, ICustomerRepository
{
    public CustomerRepository(WebShopDbContext context) : base(context) { }
    public Task UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }
}