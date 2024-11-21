using Microsoft.EntityFrameworkCore;
using WebShopSolution.Shared.Interfaces;
using WebShopSolution.Sql;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.Repositories.BaseRepoTests;
using Xunit;

namespace WebShopTests.BaseRepoTests;

public abstract class BaseRepositoryTests // Denna klassen testar BasRepots metoder, fast med ett bestämt objekt. fick inte det generiska att funka
{
    public BaseRepository<Product, int, WebShopDbContext> _baseRepository;
    public WebShopDbContext _context;

    public BaseRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<WebShopDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new WebShopDbContext(options);
        _baseRepository = new BaseRepository<Product, int, WebShopDbContext>(_context);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Name = "Product1", Description = "Description1" },
            new Product { Name = "Product2", Description = "Description2" }
        };
        await _context.Set<Product>().AddRangeAsync(products);
        await _context.SaveChangesAsync();

        // Act
        var result = await _baseRepository.GetAllAsync();

        // Assert
        Assert.Equal(products, result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntity()
    {
        // Arrange
        var product = new Product { Name = "Product", Description = "Description" };
        var idProperty = typeof(Product).GetProperty("Id");
        idProperty.SetValue(product, 1);
        await _context.Set<Product>().AddAsync(product);
        await _context.SaveChangesAsync();

        // Act
        var result = await _baseRepository.GetByIdAsync((int)idProperty.GetValue(product));

        // Assert
        Assert.Equal(product, result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntity()
    {
        // Arrange
        var product = new Product
        {
            Name = "Product",
            Description = "Description"
        };

        // Act
        await _baseRepository.AddAsync(product);
        await _context.SaveChangesAsync();

        // Assert
        var addedProduct = await _context.Products.FindAsync(product.Id);
        Assert.Equal(product, addedProduct);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntity()
    {
        // Arrange
        var product = new Product { Name = "Product", Description = "Description" };
        var idProperty = typeof(Product).GetProperty("Id");
        idProperty.SetValue(product, 1);
        await _context.Set<Product>().AddAsync(product);
        await _context.SaveChangesAsync();

        // Act
        await _baseRepository.DeleteAsync((int)idProperty.GetValue(product));
        await _context.SaveChangesAsync();

        // Assert
        var deletedProduct = await _context.Set<Product>().FindAsync((int)idProperty.GetValue(product));
        Assert.Null(deletedProduct);
    }
}