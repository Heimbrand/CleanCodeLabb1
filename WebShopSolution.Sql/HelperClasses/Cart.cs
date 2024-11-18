namespace WebShopSolution.Sql.HelperClasses;

public class Cart
{
    public int CustomerId { get; set; }
    public List<int> ProductIds { get; set; }
}