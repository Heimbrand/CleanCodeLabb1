using WebShopSolution.Sql.HelperClasses;

namespace WebShopSolution.Sql.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = default!; 
    public string Description { get; set; } = default!;
   
}