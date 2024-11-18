using WebShop.Models;


namespace WebShop.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork
    {
         // Repository för produkter
         // Sparar förändringar (om du använder en databas)
        void NotifyProductAdded(DtoProduct dtoProduct); // Notifierar observatörer om ny produkt
    }
}

