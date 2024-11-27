using Microsoft.EntityFrameworkCore;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.Repositories.BaseRepoTests;
using WebShopSolution.Sql;
using FakeItEasy;

namespace WebShopTests.BaseRepoTests
{
    public class BaseRepositoryCustomerTests
    {
        public BaseRepository<Customer, int, WebShopDbContext> BaseRepository;
        public WebShopDbContext _context;

        private void InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<WebShopDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new WebShopDbContext(options);
            BaseRepository = new BaseRepository<Customer, int, WebShopDbContext>(_context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            InitializeDatabase();
            var customers = new List<Customer>
            {
                new() { Name = "Kalle", Email = "Kalle.Anka@Hotmail.se" },
                new() { Name = "Staffan", Email = "Staffan.Stalledräng@Hotmail.se" },
                new() { Name = "Donald", Email = "Donald.Trump@Hotmail.se" },
            };
            await _context.Set<Customer>().AddRangeAsync(customers);
            await _context.SaveChangesAsync();

            // Act
            var result = await BaseRepository.GetAllAsync();

            // Assert
            Assert.Equal(customers, result);
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
            var customer = new Customer() { Name = "Pelle", Email = "Pelle.Pellesson@Hotmail.se" };

            await _context.Set<Customer>().AddAsync(customer);
            await _context.SaveChangesAsync();

            // Act
            var result = await BaseRepository.GetByIdAsync(customer.Id);

            // Assert
            Assert.Equal(customer, result);
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
            var customer = new Customer() { Name = "Pelle", Email = "Pelle.Pellesson@Hotmail.se" };

            // Act
            await BaseRepository.AddAsync(customer);
            await _context.SaveChangesAsync();

            // Assert
            var addedCustomer = await _context.Customers.FindAsync(customer.Id);
            Assert.Equal(customer, addedCustomer);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException()
        {
            // Arrange
            InitializeDatabase();
            var customer = new Customer() { Name = "Pelle"};

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () =>
            {
                await BaseRepository.AddAsync(customer);
                await _context.SaveChangesAsync();
            });
        }
        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntity()
        {
            // Arrange
            InitializeDatabase();
            var customer = new Customer() { Name = "Pelle", Email = "Pelle.Pellesson@Hotmail.se" };

            await _context.Set<Customer>().AddAsync(customer);
            await _context.SaveChangesAsync();

            // Act
            await BaseRepository.DeleteAsync(customer.Id);
            await _context.SaveChangesAsync();

            // Assert
            var deletedCustomer = await _context.Set<Customer>().FindAsync(customer.Id);
            Assert.Null(deletedCustomer);
        }
        [Fact]
        public async Task DeleteAsync_ShouldNotDeleteEntity()
        {
            // Arrange
            InitializeDatabase();
            var customer = new Customer() { Name = "Pelle", Email = "Pelles.Pellesson@Hotmail.se" };

            await _context.Set<Customer>().AddAsync(customer);
            await _context.SaveChangesAsync();

            // Act
            await BaseRepository.DeleteAsync(0);
            await _context.SaveChangesAsync();

            // Assert
            var deletedCustomer = await _context.Set<Customer>().FindAsync(customer.Id);
            Assert.NotNull(deletedCustomer);
        }
    }
}







