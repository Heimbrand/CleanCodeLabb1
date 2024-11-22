﻿using Microsoft.EntityFrameworkCore;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.Repositories.BaseRepoTests;
using WebShopSolution.Sql;

namespace WebShopTests.BaseRepoTests;

public class BaseRepositoryCustomerTests
{
    public BaseRepository<Customer, int, WebShopDbContext> BaseRepository;
    public WebShopDbContext _context;

    public BaseRepositoryCustomerTests()
    {
        var options = new DbContextOptionsBuilder<WebShopDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new WebShopDbContext(options);
        BaseRepository = new BaseRepository<Customer, int, WebShopDbContext>(_context);
    }
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var customers = new List<Customer>
        {
            new() {Name = "Kalle", Email = "Kalle.Anka@Hotmail.se" },
            new() {Name = "Staffan", Email = "Staffan.Stalledräng@Hotmail.se" },
            new() {Name = "Donald", Email = "Donald.Trump@Hotmail.se" },
        };
        await _context.Set<Customer>().AddRangeAsync(customers);
        await _context.SaveChangesAsync();

        // Act
        var result = await BaseRepository.GetAllAsync();

        // Assert
        Assert.Equal(customers, result);
    }
    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntity()
    {
        // Arrange
        var customer = new Customer() {Name = "Pelle", Email = "Pelle.Pellesson@Hotmail.se" };
        
        await _context.Set<Customer>().AddAsync(customer);
        await _context.SaveChangesAsync();

        // Act
        var result = await BaseRepository.GetByIdAsync(customer.Id);

        // Assert
        Assert.Equal(customer, result);
    }
    [Fact]
    public async Task AddAsync_ShouldAddEntity()
    {
        // Arrange
        var customer = new Customer() { Name = "Pelle", Email = "Pelle.Pellesson@Hotmail.se" };

        // Act
        await BaseRepository.AddAsync(customer);
        await _context.SaveChangesAsync();

        // Assert
        var addedCustomer = await _context.Customers.FindAsync(customer.Id);
        Assert.Equal(customer, addedCustomer);
    }
    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntity()
    {
        // Arrange
        var customer = new Customer() { Name = "Pelle", Email = "Pelle.Pellesson@Hotmail.se" };

        await _context.Set<Customer>().AddAsync(customer);
        await _context.SaveChangesAsync();

        // Act
        await BaseRepository.DeleteAsync(customer.Id);
        await _context.SaveChangesAsync();

        // Assert
        var deletedCustomer = await _context.Set<Product>().FindAsync(customer.Id);
        Assert.Null(deletedCustomer);
    }
}