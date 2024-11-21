using Microsoft.EntityFrameworkCore;
using Moq;
using WebShopSolution.Sql;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;
using WebShopSolution.Sql.Repositories;

namespace WebShopTests.RepositoryTests;

public class OrderRepositoryTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly Mock<WebShopDbContext> _mockContext;
    private readonly Mock<DbSet<Order>> _mockDbSet;

    public OrderRepositoryTests()
    {
        _mockDbSet = new Mock<DbSet<Order>>();
        _mockContext = new Mock<WebShopDbContext>(new DbContextOptions<WebShopDbContext>());
        _mockContext.Setup(m => m.Set<Order>()).Returns(_mockDbSet.Object);
        _orderRepository = new OrderRepository(_mockContext.Object);
    }
    [Fact]
    public async Task UpdateOrder_ShouldSucceed()
    {
        // Arrange
        var fakeOrder = new Order
        {
            Id = 1,
            CustomerId = 1,
            ShippingDate = DateTime.UtcNow,
            OrderProducts = new List<OrderProduct>
            {
                new()
                {
                    ProductId = 1,
                    Quantity = 1
                }
            }
        };

        // Act
        await _orderRepository.UpdateOrder(fakeOrder);

        // Assert

        _mockDbSet.Verify(m => m.Update(It.IsAny<Order>()), Times.Once());
    }
    [Fact]
    public async Task UpdateOrder_OrderNotFound_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var fakeOrder = new Order
        {
            Id = 1,
            CustomerId = 1,
            ShippingDate = DateTime.UtcNow,
            OrderProducts = new List<OrderProduct>
            {
                new()
                {
                    ProductId = 1,
                    Quantity = 1
                }
            }
        };
        _mockDbSet.Setup(m => m.Update(It.IsAny<Order>())).Throws(new InvalidOperationException());
        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _orderRepository.UpdateOrder(fakeOrder));
    }
    [Fact]
    public async Task UpdateOrder_ValidOrder_ShouldUpdateProperties()
    {
        //Arrange
        var fakeOrder = new Order
        {
            Id = 1,
            CustomerId = 1,
            ShippingDate = DateTime.UtcNow,
            OrderProducts = new List<OrderProduct>
            {
                new()
                {
                    ProductId = 1,
                    Quantity = 1
                }
            }
        };
        _mockDbSet.Setup(m => m.Update(It.IsAny<Order>())).Callback<Order>(x =>
        {
            x.CustomerId = 2;
            x.ShippingDate = DateTime.UtcNow.AddDays(1);
          
        });
        //Act
        await _orderRepository.UpdateOrder(fakeOrder);
        //Assert
        Assert.Equal(2, fakeOrder.CustomerId);
        Assert.Equal(DateTime.UtcNow.AddDays(1).Date, fakeOrder.ShippingDate.Date);
    }
}