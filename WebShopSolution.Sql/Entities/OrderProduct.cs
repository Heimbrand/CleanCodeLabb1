using WebShopSolution.Sql.HelperClasses;

namespace WebShopSolution.Sql.Entities;

public class OrderProduct : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public Product Product { get; set; } = default!;
}