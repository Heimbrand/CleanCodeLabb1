﻿using WebShop.Extensions;
using WebShopSolution.Sql.Entities;
using WebShopSolution.Sql.InterfaceRepos;
using FakeItEasy;
using Microsoft.AspNetCore.Routing;
using WebShop.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebShopTests.ExtensionTests;

public class ProductEndpointTests
{
    [Fact]
    public async Task GetAllPrododucts_ShouldReturnAllProducts()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeProduct = new List<Product>
        {
            new Product { Id = 1, Name = "Snus", Description = "Nikotin"},
            new Product { Id = 2, Name = "Pizza", Description = "Mat"}
        };
        A.CallTo(() => fakeUnitOfWork.Products.GetAllAsync()).Returns(Task.FromResult(fakeProduct.AsEnumerable()));

        // Act
        var result = await ProductEndpointExtensions.GetAllProducts(fakeUnitOfWork);

        // Assert
        var okResult = Assert.IsType<Ok<IEnumerable<Product>>>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }
    [Fact]
    public async Task GetProductById_ShouldReturnProductById()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeProduct = new Product { Id = 1, Name = "Snus", Description = "Nikotin" };
        A.CallTo(() => fakeUnitOfWork.Products.GetByIdAsync(1)).Returns(Task.FromResult(fakeProduct));

        // Act
        var result = await ProductEndpointExtensions.GetProductById(fakeUnitOfWork, 1);

        // Assert
        var okResult = Assert.IsType<Ok<Product>>(result);
        var returnValue = Assert.IsType<Product>(okResult.Value);
        Assert.Equal("Snus", returnValue.Name);
        Assert.Equal("Nikotin", returnValue.Description);
        Assert.Equal(1, returnValue.Id);
    }
    [Fact]
    public async Task AddProduct_ShouldAddProduct()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeProduct = new Product { Id = 1, Name = "Snus", Description = "Nikotin" };
        // Act
        var result = await ProductEndpointExtensions.AddProduct(fakeProduct, fakeUnitOfWork);
        // Assert
        A.CallTo(() => fakeUnitOfWork.Products.AddAsync(fakeProduct)).MustHaveHappened();
        A.CallTo(() => fakeUnitOfWork.CommitAsync()).MustHaveHappened();
    }
    [Fact]
    public async Task UpdateProduct_ShouldUpdateProduct()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeProduct = new Product { Id = 1, Name = "Snus", Description = "Nikotin" };
        // Act
        var result = await ProductEndpointExtensions.UpdateProduct(fakeProduct, fakeUnitOfWork);
        // Assert
        A.CallTo(() => fakeUnitOfWork.Products.UpdateProduct(fakeProduct)).MustHaveHappened();
        A.CallTo(() => fakeUnitOfWork.CommitAsync()).MustHaveHappened();
    }
    [Fact]
    public async Task DeleteProduct_ShouldDeleteProduct()
    {
        // Arrange
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        // Act
        var result = await ProductEndpointExtensions.DeleteProduct(fakeUnitOfWork, 1);
        // Assert
        A.CallTo(() => fakeUnitOfWork.Products.DeleteAsync(1)).MustHaveHappened();
        A.CallTo(() => fakeUnitOfWork.CommitAsync()).MustHaveHappened();
    }
}