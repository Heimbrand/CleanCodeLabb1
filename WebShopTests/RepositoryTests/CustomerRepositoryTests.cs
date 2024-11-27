//using FakeItEasy;

using Microsoft.EntityFrameworkCore;
using Moq;
using WebShopSolution.Sql;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;
using WebShopSolution.Sql.Repositories;

namespace WebShopTests.RepositoryTests
{
    public class CustomerRepositoryTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly Mock<WebShopDbContext> _mockContext;
        private readonly Mock<DbSet<Customer>> _mockDbSet;

        public CustomerRepositoryTests()
        {
            _mockDbSet = new Mock<DbSet<Customer>>();
            _mockContext = new Mock<WebShopDbContext>(new DbContextOptions<WebShopDbContext>());
            _mockContext.Setup(m => m.Set<Customer>()).Returns(_mockDbSet.Object);
            _customerRepository = new CustomerRepository(_mockContext.Object);
        }
        [Fact]
        public async Task UpdateCustomer_ShouldSucceed()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "test", Email = "Test.Test@Test.se" };

            // Act
            await _customerRepository.UpdateCustomer(customer);

            // Assert
            _mockDbSet.Verify(m => m.Update(It.IsAny<Customer>()), Times.Once());
        }
        [Fact]
        public async Task UpdateCustomer_CustomerNotFound_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "test", Email = "Test.Test@Test.se" };
            _mockDbSet.Setup(m => m.Update(It.IsAny<Customer>())).Throws(new InvalidOperationException());

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _customerRepository.UpdateCustomer(customer));
        }
        [Fact]
        public async Task UpdateCustomer_ValidCustomer_ShouldUpdateProperties()
        {
            //Arrange
            var Customer = new Customer { Id = 1, Name = "test", Email = "Test.Test@Test.se" };
            _mockDbSet.Setup(m => m.Update(It.IsAny<Customer>())).Callback<Customer>(x =>
            {
                x.Name = "Updated";
                x.Email = "Updated.Updated@Updated.se";
            });

            //Act
            await _customerRepository.UpdateCustomer(Customer);

            //Assert
            Assert.Equal("Updated", Customer.Name);
            Assert.Equal("Updated.Updated@Updated.se", Customer.Email);
        }
    }
}