using Microsoft.EntityFrameworkCore;
using WebShopSolution.Sql.Entities;

namespace WebShopSolution.Sql;

public class WebShopDbContext : DbContext
{
    public WebShopDbContext(DbContextOptions<WebShopDbContext> options) : base(options)
    {
    }


    public DbSet<Product> Products { get; set; }


}