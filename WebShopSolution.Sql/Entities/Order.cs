using WebShopSolution.Sql.HelperClasses;

namespace WebShopSolution.Sql.Entities;

public class Order : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;
    public List<Product>? Products { get; set; } 
    public DateTime ShippingDate { get; set; }
}