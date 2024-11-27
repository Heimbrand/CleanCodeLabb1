using Microsoft.EntityFrameworkCore;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.Repositories.BaseRepoTests;
using WebShopSolution.Sql;
using Castle.Core.Resource;

namespace WebShopTests.BaseRepoTests;

public class BaseRepositoryOrderTests
{
    public BaseRepository<Order, int, WebShopDbContext> BaseRepository;
    public WebShopDbContext _context;

    private void InitializeDatabase()
    {
        var options = new DbContextOptionsBuilder<WebShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new WebShopDbContext(options);
        BaseRepository = new BaseRepository<Order, int, WebShopDbContext>(_context);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        InitializeDatabase();
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
    public async Task GetAllAsync_ShouldReturnEmptyList()
    {
        // Arrange
        InitializeDatabase();

        // Act
        var result = await BaseRepository.GetAllAsync();

        // Assert
        Assert.Empty(result);
    }
    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntity()
    {
        // Arrange
        InitializeDatabase();
        var order = new Order { CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct> { new() { ProductId = 1, Quantity = 1 } } };

        await _context.Set<Order>().AddAsync(order);
        await _context.SaveChangesAsync();

        // Act
        var result = await BaseRepository.GetByIdAsync(order.Id);

        // Assert
        Assert.Equal(order, result);
    }
    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull()
    {
        // Arrange
        InitializeDatabase();
        // Act
        var result = await BaseRepository.GetByIdAsync(1);
        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task AddAsync_ShouldAddEntity()
    {
        // Arrange
        InitializeDatabase();
        var order = new Order { CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct> { new() { ProductId = 1, Quantity = 1 } } };

        // Act
        await BaseRepository.AddAsync(order);
        await _context.SaveChangesAsync();

        // Assert
        var addedOrder = await _context.Orders.FindAsync(order.Id);
        Assert.Equal(order, addedOrder);
    }
    [Fact]
    public async Task AddAsync_ShouldThrowException()
    {
        // Arrange
        InitializeDatabase();
        var order = new Order { CustomerId = -1 };

        // Act & Assert
        await Assert.ThrowsAsync<DbUpdateException>(async () =>
        {
            
            if (!await _context.Customers.AnyAsync(c => c.Id == order.CustomerId))
            {
                throw new DbUpdateException("Customer not found", new Exception());
            }
        });
    }
    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntity()
    {
        // Arrange
        InitializeDatabase();
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
    [Fact]
    public async Task DeleteAsync_ShouldThrowException()
    {
        // Arrange
        InitializeDatabase();
        var order = new Order { CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct> { new() { ProductId = 1, Quantity = 1 } } };
        await _context.Set<Order>().AddAsync(order);
        await _context.SaveChangesAsync();

        // Act
        await BaseRepository.DeleteAsync(0);
        await _context.SaveChangesAsync();

        // Assert
        var deletedOrder = await _context.Set<Order>().FindAsync(order.Id);
        Assert.NotNull(deletedOrder);
    }
}