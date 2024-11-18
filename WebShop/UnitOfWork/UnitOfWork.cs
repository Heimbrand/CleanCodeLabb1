using Microsoft.EntityFrameworkCore;
using WebShop.Models;
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
        private readonly DbSet<Product> _dbSet;
        public IProductRepository Products { get; private set; }

        private readonly ProductSubject _productSubject;

        // Konstruktor används för tillfället av Observer pattern
        public UnitOfWork(WebShopDbContext context)
        {
           
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
        public void NotifyProductAdded(Product product)
        {
            _productSubject.Notify(product);
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}
