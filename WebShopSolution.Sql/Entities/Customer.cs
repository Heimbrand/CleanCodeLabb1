using System.ComponentModel.DataAnnotations;
using WebShopSolution.Sql.HelperClasses;

namespace WebShopSolution.Sql.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = default!;
    [EmailAddress]
    public string Email { get; set; } = default!;
}