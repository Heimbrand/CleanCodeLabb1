using WebShopSolution.Shared.Interfaces;

namespace WebShopSolution.Sql.HelperClasses;

public class BaseEntity : IEntity<int>
{
    public int Id { get; set; }
}