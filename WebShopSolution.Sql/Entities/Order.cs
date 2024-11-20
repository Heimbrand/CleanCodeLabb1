using WebShopSolution.Sql.HelperClasses;

namespace WebShopSolution.Sql.Entities;

public class Order : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;
    public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public DateTime ShippingDate { get; set; }
}