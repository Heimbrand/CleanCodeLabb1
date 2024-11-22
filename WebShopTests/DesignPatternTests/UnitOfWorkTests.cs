using Microsoft.EntityFrameworkCore;
using Moq;
using WebShop.Notifications;
using WebShop.UnitOfWork;
using WebShopSolution.Sql;
using WebShopSolution.Sql.Entities;

public class UnitOfWorkTests
{
    private readonly Mock<INotificationObserver> _observerMock;
    private readonly ProductSubject _productSubject;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkTests()
    {
        _observerMock = new Mock<INotificationObserver>();
        _productSubject = new ProductSubject();
        var dummyContext = new Mock<WebShopDbContext>(new DbContextOptions<WebShopDbContext>()).Object;
        _unitOfWork = new UnitOfWork(dummyContext, _productSubject);
    }
    [Fact]
    public void NotifyProductAdded_CallsObserverUpdate()
    {
        // Arrange
        var product = new Product {Id = 1, Name = "TestarObserver", Description = "OchStrategy"};

        _unitOfWork.AttachObserver(_observerMock.Object);

        // Act

        _unitOfWork.NotifyObserver(product);

        // Assert

        _observerMock.Verify(o => o.Update(product), Times.Once);
    }
    [Fact]
    public void AttachObserver_CallsAttachOnProductSubject()
    {
        // Arrange
        _unitOfWork.AttachObserver(_observerMock.Object);

        // Act & Assert
        _unitOfWork.NotifyObserver(new Product { Id = 1, Name = "test", Description = "test" });
        _observerMock.Verify(o => o.Update(It.IsAny<Product>()), Times.Once);
    }
    [Fact]
    public void DetachObserver_CallsDetachOnProductSubject()
    {
        // Arrange
        _unitOfWork.AttachObserver(_observerMock.Object);
        _unitOfWork.DetachObserver(_observerMock.Object);
        // Act & Assert
        _productSubject.Notify(new Product { Id = 1, Name = "test", Description = "test" });
        _observerMock.Verify(o => o.Update(It.IsAny<Product>()), Times.Never);
    }
}

