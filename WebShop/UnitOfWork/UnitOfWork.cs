using WebShop.Notifications;
using WebShopSolution.Sql;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;
using WebShopSolution.Sql.Repositories;

namespace WebShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly WebShopDbContext _context;
        private readonly ProductSubject _productSubject;
        public IProductRepository Products { get;}
        public IOrderRepository Orders { get;}
        public ICustomerRepository Customers { get;}

        public UnitOfWork(WebShopDbContext context, ProductSubject productSubject)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Products = new ProductRepository(_context);
            Orders = new OrderRepository(_context);
            Customers = new CustomerRepository(_context);
            _productSubject = productSubject ?? throw new ArgumentNullException(nameof(productSubject));
        }
        public void AttachObserver(INotificationObserver observer)
        {
            _productSubject.Attach(observer);
        }
        public void DetachObserver(INotificationObserver observer)
        {
            _productSubject.Detach(observer);
        }
        public void NotifyObserver(Product product)
        {
            _productSubject.Notify(product);
        }
        public int CommitAsync()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
