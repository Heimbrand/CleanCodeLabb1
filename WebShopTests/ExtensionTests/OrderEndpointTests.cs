using FakeItEasy;
using Microsoft.AspNetCore.Http.HttpResults;
using WebShop.Extensions;
using WebShop.UnitOfWork;
using WebShopSolution.Sql.Entities;

namespace WebShopTests.ExtensionTests;

public class OrderEndpointTests
{
    [Fact]
    public async Task GetAllOrders_ShouldReturnAllOrders()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeOrder = A.CollectionOfDummy<Order>(4);
        A.CallTo(() => fakeUnitOfWork.Orders.GetAllAsync()).Returns(Task.FromResult(fakeOrder.AsEnumerable()));
        // Act
        var result = await OrderEndpointExtensions.GetAllOrders(fakeUnitOfWork);
        // Assert
        var okResult = Assert.IsType<Ok<IEnumerable<Order>>>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<Order>>(okResult.Value);
        Assert.Equal(4, returnValue.Count());
    }
    [Fact]
    public async Task GetAllOrders_ShouldReturnNotFound()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(() => fakeUnitOfWork.Orders.GetAllAsync()).Returns(Task.FromResult((IEnumerable<Order>)null));

        // Act
        var result = await OrderEndpointExtensions.GetAllOrders(fakeUnitOfWork);

        // Assert
        var notOkResult = Assert.IsType<NotFound>(result);
    }
    [Fact]
    public async Task GetOrderById_ShouldReturnOrderById()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeOrder = new Order { Id = 1, CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct> { new() { ProductId = 1, Quantity = 1 } } };
        A.CallTo(() => fakeUnitOfWork.Orders.GetByIdAsync(1)).Returns(Task.FromResult(fakeOrder));
        // Act
        var result = await OrderEndpointExtensions.GetOrderById(fakeUnitOfWork, 1);
        // Assert
        var okResult = Assert.IsType<Ok<Order>>(result);
        var returnValue = Assert.IsType<Order>(okResult.Value);
        Assert.Equal(1, returnValue.Id);
        Assert.Equal(1, returnValue.CustomerId);
        Assert.Equal(1, returnValue.OrderProducts.Count());
    }
    [Fact]
    public async Task GetOrderById_ShouldReturnNotFound()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(() => fakeUnitOfWork.Orders.GetByIdAsync(1)).Returns(Task.FromResult((Order)null));
        // Act
        var result = await OrderEndpointExtensions.GetOrderById(fakeUnitOfWork, 1);
        // Assert
        var notOkResult = Assert.IsType<NotFound>(result);
    }
    [Fact]
    public async Task AddOrder_ShouldAddOrder()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeOrder = new Order { Id = 1, CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct> { new() { ProductId = 1, Quantity = 1 } } };
        // Act
        var result = await OrderEndpointExtensions.AddOrder(fakeOrder, fakeUnitOfWork);
        // Assert
        var okResult = Assert.IsType<Ok<Order>>(result);
        var returnValue = Assert.IsType<Order>(okResult.Value);
        Assert.Equal(1, returnValue.Id);
        Assert.Equal(1, returnValue.CustomerId);
        Assert.Equal(1, returnValue.OrderProducts.Count());
    }
    [Fact]
    public async Task AddOrder_ShouldReturnBadRequest()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeOrder = new Order { Id = 1, CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct> { new() { ProductId = 1, Quantity = 1 } } };
        A.CallTo(() => fakeUnitOfWork.Customers.GetByIdAsync(1)).Returns(Task.FromResult((Customer)null));
        // Act
        var result = await OrderEndpointExtensions.AddOrder(fakeOrder, fakeUnitOfWork);
        // Assert
        var badRequestResult = Assert.IsType<BadRequest>(result);
    }
    [Fact]
    public async Task AddOrder_ShouldReturnBadRequestIfOrderProductsIsNull()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeOrder = new Order { Id = 1, CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = null };
        // Act
        var result = await OrderEndpointExtensions.AddOrder(fakeOrder, fakeUnitOfWork);
        // Assert
        var badRequestResult = Assert.IsType<BadRequest>(result);
    }
    [Fact]
    public async Task UpdateOrder_ShouldUpdateOrder()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeOrder = new Order { Id = 1, CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct> { new() { ProductId = 1, Quantity = 1 } } };
        // Act
        var result = await OrderEndpointExtensions.UpdateOrder(fakeOrder, fakeUnitOfWork);
        // Assert
        var okResult = Assert.IsType<Ok<Order>>(result);
        var returnValue = Assert.IsType<Order>(okResult.Value);
        Assert.Equal(1, returnValue.Id);
        Assert.Equal(1, returnValue.CustomerId);
        Assert.Equal(1, returnValue.OrderProducts.Count());
    }
    [Fact]
    public async Task UpdateOrder_ShouldReturnBadRequest()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeOrder = new Order { Id = 1, CustomerId = 1, ShippingDate = DateTime.UtcNow, OrderProducts = new List<OrderProduct> { new() { ProductId = 1, Quantity = 1 } } };
        A.CallTo(() => fakeUnitOfWork.Orders.GetByIdAsync(1)).Returns(Task.FromResult((Order)null!));
        // Act
        var result = await OrderEndpointExtensions.UpdateOrder(fakeOrder, fakeUnitOfWork);
        // Assert
        var badRequestResult = Assert.IsType<BadRequest>(result);
    }
    [Fact]
    public async Task DeleteOrder_ShouldDeleteOrder()
    {
       // Arrange
       var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        // Act
        var result = await OrderEndpointExtensions.DeleteOrder(fakeUnitOfWork, 1);
        // Assert
        A.CallTo(() => fakeUnitOfWork.Orders.DeleteAsync(1)).MustHaveHappened();
        A.CallTo(() => fakeUnitOfWork.CommitAsync()).MustHaveHappened();
    }
    [Fact]
    public async Task DeleteOrder_ShouldReturnBadRequest()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(() => fakeUnitOfWork.Orders.GetByIdAsync(1)).Returns(Task.FromResult((Order)null));
        // Act
        var result = await OrderEndpointExtensions.DeleteOrder(fakeUnitOfWork, 1);
        // Assert
        var badRequestResult = Assert.IsType<BadRequest>(result);
    }
}