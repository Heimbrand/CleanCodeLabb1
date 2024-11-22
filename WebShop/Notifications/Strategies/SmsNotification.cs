using WebShopSolution.Sql.Entities;

namespace WebShop.Notifications.Strategies;

public class SmsNotification : INotificationObserver
{
    public void Update(Product product)
    {
        Console.WriteLine($"Sms: Product {product.Name} has been added to the shop");
    }
}