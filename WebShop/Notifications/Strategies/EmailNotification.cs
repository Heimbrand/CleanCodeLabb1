using WebShop.Notifications;
using WebShopSolution.Sql.Entities;

public class EmailNotification : INotificationObserver
{
    public void Update(Product product)
    {
        Console.WriteLine($"Email: Product {product.Name} has been added to the shop");
    }
}





