using WebShop.Models;

namespace WebShop.Notifications
{
    // En konkret observatör som skickar e-postmeddelanden
    public class EmailNotification : INotificationObserver
    {
        public void Update(DtoProduct dtoProduct)
        {
            // Här skulle du implementera logik för att skicka ett e-postmeddelande
            // För enkelhetens skull skriver vi ut till konsolen
            Console.WriteLine($"Email Notification: New dtoProduct added - {dtoProduct.Name}");
        }
    }
}
