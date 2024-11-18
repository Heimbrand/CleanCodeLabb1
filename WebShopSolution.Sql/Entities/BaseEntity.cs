using WebShopSolution.Shared.Interfaces;

namespace WebShopSolution.Sql.Entities;

public class BaseEntity : IEntity<int>
{
    public int Id { get; set; }
}