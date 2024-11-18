using WebShop.Models;
using WebShopSolution.Sql.Entities;

namespace WebShop.Notifications
{
    // Gränssnitt för notifieringsobservatörer enligt Observer Pattern
    public interface INotificationObserver
    {
        void Update(Product dtoProduct); // Metod som kallas när en ny produkt läggs till
    }
}
