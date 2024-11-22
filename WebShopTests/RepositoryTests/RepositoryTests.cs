using Microsoft.EntityFrameworkCore;
using Moq;
using WebShopSolution.Sql;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;
using WebShopSolution.Sql.Repositories;

namespace WebShopTests.RepositoryTests;

public class RepositoryTests
{
    private readonly IProductRepository _productRepository;
    private readonly Mock<WebShopDbContext> _mockContext;
    private readonly Mock<DbSet<Product>> _mockDbSet;

    public RepositoryTests()
    {
        _mockDbSet = new Mock<DbSet<Product>>();
        _mockContext = new Mock<WebShopDbContext>(new DbContextOptions<WebShopDbContext>());
        _mockContext.Setup(m => m.Set<Product>()).Returns(_mockDbSet.Object);
        _productRepository = new Repository(_mockContext.Object);
    }
    [Fact]
    public async Task UpdateProduct_ShouldSucceed()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Julgran", Description = "God Jul"};
        // Act
        await _productRepository.UpdateProduct(product);
        // Assert
        _mockDbSet.Verify(m => m.Update(It.IsAny<Product>()), Times.Once());
    }
    [Fact]
    public async Task UpdateProduct_ProductNotFound_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Julgran", Description = "God Jul" };
        _mockDbSet.Setup(m => m.Update(It.IsAny<Product>())).Throws(new InvalidOperationException());
        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _productRepository.UpdateProduct(product));
    }
    [Fact]
    public async Task UpdateProduct_ValidProduct_ShouldUpdateProperties()
    {
        //Arrange
        var product = new Product { Id = 1, Name = "Julgran", Description = "God Jul" };
        _mockDbSet.Setup(m => m.Update(It.IsAny<Product>())).Callback<Product>(x =>
        {
            x.Name = "Inte gran";
            x.Description = "Glad påsk";
        });
        //Act
        await _productRepository.UpdateProduct(product);
        //Assert
        Assert.Equal("Inte gran", product.Name);
        Assert.Equal("Glad påsk", product.Description);
    }
}