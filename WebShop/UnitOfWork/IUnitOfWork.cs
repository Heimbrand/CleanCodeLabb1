using WebShop.Notifications;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;


namespace WebShop.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }

        void AttachObserver(INotificationObserver observer);
        void DetachObserver(INotificationObserver observer);
        void NotifyObserver(Product product);
        int CommitAsync();
    }
}

