using WebShopSolution.Sql.Entities;

namespace WebShop.Notifications
{
   
    public interface INotificationObserver
    {
        void Update(Product product); 
    }
}
