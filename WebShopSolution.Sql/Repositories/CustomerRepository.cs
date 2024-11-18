using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;

namespace WebShopSolution.Sql.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public Task<IEnumerable<Customer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}