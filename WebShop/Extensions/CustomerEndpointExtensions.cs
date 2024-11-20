﻿using Microsoft.AspNetCore.Mvc;
using WebShop.UnitOfWork;
using WebShopSolution.Sql.Entities;

namespace WebShop.Extensions;

public static class CustomerEndpointExtensions
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/customers");
        group.MapGet("", GetAllCustomers);
        group.MapGet("{id}", GetCustomerById);
        group.MapPost("", AddCustomer);
        group.MapPut("", UpdateCustomer);
        group.MapDelete("{id}", DeleteCustomer);

        return app;
    }

    #region Api methods
    private static async Task<IResult> GetAllCustomers([FromServices] IUnitOfWork unitOfWork)
    {
        try
        {
            var customers = await unitOfWork.Customers.GetAllAsync();
            return customers is null ? Results.NotFound() : Results.Ok(customers);
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
    }
    private static async Task<IResult> GetCustomerById([FromServices] IUnitOfWork unitOfWork, int id)
    {
        try
        {
            var customer = await unitOfWork.Customers.GetByIdAsync(id);
            return customer is null ? Results.NotFound() : Results.Ok(customer);
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
    }
    private static async Task<IResult> AddCustomer([FromBody] Customer customer, [FromServices] IUnitOfWork unitOfWork)
    {
        if (customer is null)
            return Results.BadRequest("Customer is null");
        try
        {
            await unitOfWork.Customers.AddAsync(customer);
            unitOfWork.complete();
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
        return Results.Created($"/api/customers/{customer.Id}", customer);
    }
    private static async Task<IResult> UpdateCustomer([FromBody] Customer customer, [FromServices] IUnitOfWork unitOfWork)
    {
        if (customer is null)
            return Results.BadRequest("Customer is null");
        try
        {
            await unitOfWork.Customers.UpdateCustomer(customer);
            unitOfWork.complete();
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
        return Results.Ok();
    }
    private static async Task<IResult> DeleteCustomer([FromServices] IUnitOfWork unitOfWork, int id)
    {
        try
        {
            var customer = await unitOfWork.Customers.GetByIdAsync(id);
            if (customer is null)
                return Results.NotFound();
            await unitOfWork.Customers.DeleteAsync(customer.Id);
            unitOfWork.complete();
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
        return Results.Ok();
    }
    #endregion
}