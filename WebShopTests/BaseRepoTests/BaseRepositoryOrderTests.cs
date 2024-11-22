using Microsoft.EntityFrameworkCore;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.Repositories.BaseRepoTests;
using WebShopSolution.Sql;

namespace WebShopTests.BaseRepoTests;

public class BaseRepositoryOrderTests
{
    public BaseRepository<Order, int, WebShopDbContext> BaseRepository;
    public WebShopDbContext _context;

    public BaseRepositoryOrderTests()
    {
        var options = new DbContextOptionsBuilder<WebShopDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new WebShopDbContext(options);
        BaseRepository = new BaseRepository<Order, int, WebShopDbContext>(_context);
    }
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var orders = new List<Order>
        {
            new() {CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct>{new (){ProductId = 1, Quantity = 1}}},
            new() {CustomerId = 2, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct>{new (){ProductId = 2, Quantity = 2}}}
        };
        await _context.Set<Order>().AddRangeAsync(orders);
        await _context.SaveChangesAsync();

        // Act
        var result = await BaseRepository.GetAllAsync();

        // Assert
        Assert.Equal(orders, result);
    }
    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntity()
    {
        // Arrange
        var order = new Order { CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct> { new() { ProductId = 1, Quantity = 1 } } };

        await _context.Set<Order>().AddAsync(order);
        await _context.SaveChangesAsync();

        // Act
        var result = await BaseRepository.GetByIdAsync(order.Id);

        // Assert
        Assert.Equal(order, result);
    }
    [Fact]
    public async Task AddAsync_ShouldAddEntity()
    {
        // Arrange
        var order = new Order { CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct> { new() { ProductId = 1, Quantity = 1 } } };

        // Act
        await BaseRepository.AddAsync(order);
        await _context.SaveChangesAsync();

        // Assert
        var addedOrder = await _context.Orders.FindAsync(order.Id);
        Assert.Equal(order, addedOrder);
    }
    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntity()
    {
        // Arrange
        var order = new Order { CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct> { new() { ProductId = 1, Quantity = 1 } } };

        await _context.Set<Order>().AddAsync(order);
        await _context.SaveChangesAsync();

        // Act
        await BaseRepository.DeleteAsync(order.Id);
        await _context.SaveChangesAsync();

        // Assert
        var deletedOrder = await _context.Set<Order>().FindAsync(order.Id);
        Assert.Null(deletedOrder);
    }
}