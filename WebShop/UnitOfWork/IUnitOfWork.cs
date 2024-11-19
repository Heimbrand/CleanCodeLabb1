using WebShopSolution.Sql.InterfaceRepos;


namespace WebShop.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }

        int complete();
    }
}

