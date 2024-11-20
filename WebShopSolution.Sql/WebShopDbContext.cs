using Microsoft.EntityFrameworkCore;
using WebShopSolution.Sql.Entities;

namespace WebShopSolution.Sql;

public class WebShopDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }


    public WebShopDbContext(DbContextOptions<WebShopDbContext> options) : base(options)
    {
    }

}