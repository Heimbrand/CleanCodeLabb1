using WebShopSolution.Sql.Entities;

namespace WebShop.Notifications.Strategies;

public class SmokeSignalNotification : INotificationObserver
{
    public void Update(Product product)
    {
        Console.WriteLine($"SmokeSignal: Product {product.Name} has been added to the shop");
    }
}