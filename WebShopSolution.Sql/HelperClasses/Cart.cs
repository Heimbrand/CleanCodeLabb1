namespace WebShopSolution.Sql.HelperClasses;

// This class is meant to be used as a temporary cart for the customer, the contents of the cart will be moved to OrderProducts when the customer checks out.
public class Cart
{
    public int CustomerId { get; set; }
    public List<int> ProductIds { get; set; }
}