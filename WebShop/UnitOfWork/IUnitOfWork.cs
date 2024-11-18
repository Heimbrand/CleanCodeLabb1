using WebShop.Models;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;


namespace WebShop.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        
        // Sparar förändringar (om du använder en databas)
        void Save();
       
       
        void NotifyProductAdded(Product dtoProduct); // Notifierar observatörer om ny produkt
    }
}

