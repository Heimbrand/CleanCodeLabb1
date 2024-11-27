using Microsoft.EntityFrameworkCore;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.Repositories.BaseRepoTests;
using WebShopSolution.Sql;

namespace WebShopTests.BaseRepoTests;

public class BaseRepositoryProductTests
{
    public BaseRepository<Product, int, WebShopDbContext> BaseRepository;
    public WebShopDbContext _context;
    private void InitializeDatabase()
    {
        var options = new DbContextOptionsBuilder<WebShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new WebShopDbContext(options);
        BaseRepository = new BaseRepository<Product, int, WebShopDbContext>(_context);
    }
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        InitializeDatabase();
        var products = new List<Product>
        {
            new Product { Name = "Product1", Description = "Description1" },
            new Product { Name = "Product2", Description = "Description2" }
        };
        await _context.Set<Product>().AddRangeAsync(products);
        await _context.SaveChangesAsync();

        // Act
        var result = await BaseRepository.GetAllAsync();

        // Assert
        Assert.Equal(products, result);
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
        var product = new Product { Name = "Product", Description = "Description" };

        await _context.Set<Product>().AddAsync(product);
        await _context.SaveChangesAsync();

        // Act
        var result = await BaseRepository.GetByIdAsync(product.Id);

        // Assert
        Assert.Equal(product, result);
    }
    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull()
    {
        // Arrange
        InitializeDatabase();
        // Act
        var result = await BaseRepository.GetByIdAsync(0);
        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task AddAsync_ShouldAddEntity()
    {
        // Arrange
        InitializeDatabase();
        var product = new Product
        {
            Name = "Product",
            Description = "Description"
        };

        // Act
        await BaseRepository.AddAsync(product);
        await _context.SaveChangesAsync();

        // Assert
        var addedProduct = await _context.Products.FindAsync(product.Id);
        Assert.Equal(product, addedProduct);
    }
    [Fact]
    public async Task AddAsync_ShouldNotAddEntity()
    {
        // Arrange
        InitializeDatabase();
        var product = new Product { Name = "Product"};

        // Act & Assert
        await Assert.ThrowsAsync<DbUpdateException>(async () =>
        {
            await BaseRepository.AddAsync(product);
            await _context.SaveChangesAsync();
        });
    }
    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntity()
    {
        // Arrange
        InitializeDatabase();
        var product = new Product { Name = "Product", Description = "Description" };

        await _context.Set<Product>().AddAsync(product);
        await _context.SaveChangesAsync();

        // Act
        await BaseRepository.DeleteAsync(product.Id);
        await _context.SaveChangesAsync();

        // Assert
        var deletedProduct = await _context.Set<Product>().FindAsync(product.Id);
        Assert.Null(deletedProduct);
    }
    [Fact]
    public async Task DeleteAsync_ShouldNotDeleteProduct()
    {
        // Arrange
        InitializeDatabase();
        var product = new Product { Name = "Product", Description = "Description" };

        await _context.Set<Product>().AddAsync(product);
        await _context.SaveChangesAsync();

        // Act
        await BaseRepository.DeleteAsync(0);
        await _context.SaveChangesAsync();

        // Assert
        var deletedProduct = await _context.Set<Product>().FindAsync(product.Id);
        Assert.NotNull(deletedProduct);
    }
}