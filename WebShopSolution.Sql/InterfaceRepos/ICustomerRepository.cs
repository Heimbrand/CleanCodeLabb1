using WebShopSolution.Shared.Interfaces;
using WebShopSolution.Sql.Entities;

namespace WebShopSolution.Sql.InterfaceRepos;

public interface ICustomerRepository : IRepository<Customer, int>
{
    Task UpdateCustomer(Customer customer);
}