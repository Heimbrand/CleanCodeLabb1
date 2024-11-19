using WebShop.Models;
using WebShopSolution.Sql.Entities;

namespace WebShop.Notifications
{
    public class ProductSubject 
    {
        private readonly List<INotificationObserver> _observers = new();

        public void Attach(INotificationObserver observer)
        {
            
            _observers.Add(observer);
        }

        public void Detach(INotificationObserver observer)
        {
           
            _observers.Remove(observer);
        }

        public void Notify(Product dtoProduct)
        {
            
            foreach (var observer in _observers)
            {
                observer.Update(dtoProduct);
            }
        }
    }
}
