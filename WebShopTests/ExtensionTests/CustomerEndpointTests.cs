using FakeItEasy;
using Microsoft.AspNetCore.Http.HttpResults;
using WebShop.Extensions;
using WebShop.UnitOfWork;
using WebShopSolution.Sql.Entities;

namespace WebShopTests.ExtensionTests;

public class CustomerEndpointTests
{
    [Fact]
    public async Task GetAllCustomers_ShouldReturnAllCustomers()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeCustomer = new List<Customer>
        {
            new() { Id = 1, Name = "Kalle", Email = "Kalle.Anka@Hotmail.se" },
            new() { Id = 1, Name = "Staffan", Email = "Staffan.Stalledräng@Hotmail.se" },
            new() { Id = 1, Name = "Donald", Email = "Donald.Trump@Hotmail.se" },
        };
        A.CallTo(() => fakeUnitOfWork.Customers.GetAllAsync()).Returns(Task.FromResult(fakeCustomer.AsEnumerable()));

        // Act
        var result = await CustomerEndpointExtensions.GetAllCustomers(fakeUnitOfWork);

        // Assert
        var okResult = Assert.IsType<Ok<IEnumerable<Customer>>>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<Customer>>(okResult.Value);
        Assert.Equal(3, returnValue.Count());
    }
    [Fact]
    public async Task GetAllCustomers_ShouldReturnNotFound()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(() => fakeUnitOfWork.Customers.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Customer>>(null));
        // Act
        var result = await CustomerEndpointExtensions.GetAllCustomers(fakeUnitOfWork);
        // Assert
        Assert.IsType<NotFound>(result);
    }
    [Fact]
    public async Task GetCustomerById_ShouldReturnCustomerById()
    {
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeCustomer = new Customer { Id = 1, Name = "Kalle", Email = "Kalle.Anka@Hotmail.se" };
        A.CallTo(() => fakeUnitOfWork.Customers.GetByIdAsync(1)).Returns(Task.FromResult(fakeCustomer));

        // Act
        var result = await CustomerEndpointExtensions.GetCustomerById(fakeUnitOfWork, 1);

        // Assert
        var okResult = Assert.IsType<Ok<Customer>>(result);
        var returnValue = Assert.IsType<Customer>(okResult.Value);
        Assert.Equal(1, returnValue.Id);
    }
    [Fact]
    public async Task GetCustomerById_ShouldReturnNotFound()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(() => fakeUnitOfWork.Customers.GetByIdAsync(1)).Returns(Task.FromResult<Customer>(null));
        // Act
        var result = await CustomerEndpointExtensions.GetCustomerById(fakeUnitOfWork, 1);
        // Assert
        Assert.IsType<NotFound>(result);
    }
    [Fact]
    public async Task AddCustomer_ShouldAddCustomer()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeCustomer = new Customer { Id = 1, Name = "Kalle", Email = "Kalle.Anka@Hotmail.se" };

        // Act
        var result = await CustomerEndpointExtensions.AddCustomer(fakeCustomer, fakeUnitOfWork);

        // Assert
        A.CallTo(() => fakeUnitOfWork.Customers.AddAsync(fakeCustomer)).MustHaveHappened();
        A.CallTo(() => fakeUnitOfWork.CommitAsync()).MustHaveHappened();
    }
    [Fact]
    public async Task AddCustomer_ShouldReturnBadRequest()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        // Act
        var result = await CustomerEndpointExtensions.AddCustomer(null, fakeUnitOfWork);
        // Assert
        Assert.IsType<BadRequest>(result);
    }
    [Fact]
    public async Task UpdateCostumer_ShouldUpdateCostumer()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeCustomer = new Customer { Id = 1, Name = "Kalle", Email = "Kalle.Anka@Hotmail.se" };

        // Act
        var result = await CustomerEndpointExtensions.UpdateCustomer(fakeCustomer, fakeUnitOfWork);

        // Assert
        A.CallTo(() => fakeUnitOfWork.Customers.UpdateCustomer(fakeCustomer)).MustHaveHappened();
        A.CallTo(() => fakeUnitOfWork.CommitAsync()).MustHaveHappened();
    }
    [Fact]
    public async Task UpdateCustomer_ShouldReturnBadRequest()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        // Act
        var result = await CustomerEndpointExtensions.UpdateCustomer(null, fakeUnitOfWork);
        // Assert
        Assert.IsType<BadRequest>(result);
    }
    [Fact]
    public async Task DeleteCustomer_ShouldDeleteCustomer()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        // Act
        var result = await CustomerEndpointExtensions.DeleteCustomer(fakeUnitOfWork, 1);
        // Assert
        A.CallTo(() => fakeUnitOfWork.Customers.DeleteAsync(1)).MustHaveHappened();
        A.CallTo(() => fakeUnitOfWork.CommitAsync()).MustHaveHappened();
    }
    [Fact]
    public async Task DeleteCustomer_ShouldReturnBadRequest()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        // Act
        var result = await CustomerEndpointExtensions.DeleteCustomer(fakeUnitOfWork, 0);
        // Assert
        Assert.IsType<BadRequest>(result);
    }
}