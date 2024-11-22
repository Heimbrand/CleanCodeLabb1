using WebShopSolution.Sql.Entities;

namespace WebShop.Notifications.Strategies;

public class HawkNotificationDeliveryServiceMACAAW : INotificationObserver
{
    public void Update(Product product)
    {
        Console.WriteLine($"Birb: Product {product.Name} has been added to the shop");
    }
}