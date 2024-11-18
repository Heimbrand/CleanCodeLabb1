using WebShop.Models;

namespace WebShop.Notifications
{
    // Gränssnitt för notifieringsobservatörer enligt Observer Pattern
    public interface INotificationObserver
    {
        void Update(DtoProduct dtoProduct); // Metod som kallas när en ny produkt läggs till
    }
}
