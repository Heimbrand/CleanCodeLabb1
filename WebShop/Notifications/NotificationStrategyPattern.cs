using WebShop.Notifications;
using WebShopSolution.Sql.Entities;





public class NotificationStrategyPattern : INotificationObserver
{
    private readonly NotificationStrategy _notificationStrategy;

    public NotificationStrategyPattern(NotificationStrategy notificationStrategy)
    {
        _notificationStrategy = notificationStrategy;
    }
    public void Update(Product product)
    {
        _notificationStrategy.MessageStrategies(product);
    }
}





