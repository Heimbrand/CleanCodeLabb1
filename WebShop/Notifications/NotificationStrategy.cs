using WebShopSolution.Sql.Entities;

namespace WebShop.Notifications;

public class NotificationStrategy
{
    public void MessageStrategies(Product product)
    {
        var messageStrategies = new Dictionary<NotificationTypes, Action>()
        {
            { NotificationTypes.Email, () => Console.WriteLine($"NotificationType: Email, Notification: {product.Name} Has been added")},
            { NotificationTypes.SMS, () => Console.WriteLine($"NotificationType: SMS, Notification: {product.Name} Has been added")},
        };

        foreach (var strategy in messageStrategies.Values)
        {
            strategy();
        }
    }
}