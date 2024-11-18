using WebShopSolution.Sql.HelperClasses;

namespace WebShopSolution.Sql.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = default!; 
    public int Quantity { get; set; }
    
}