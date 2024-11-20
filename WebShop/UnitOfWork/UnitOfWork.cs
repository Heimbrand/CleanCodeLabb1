using Microsoft.EntityFrameworkCore;
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
        private readonly DbSet<Product> _productDbset;
        private readonly DbSet<Order> _orderDbset;
        private readonly DbSet<Customer> _customerDbset;
        private readonly ProductSubject _productSubject;
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; }
        public ICustomerRepository Customers { get; }
       

        // Konstruktor används för tillfället av Observer pattern
        public UnitOfWork(WebShopDbContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Products = new ProductRepository(_context);
            Orders = new OrderRepository(_context);
            Customers = new CustomerRepository(_context);
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
