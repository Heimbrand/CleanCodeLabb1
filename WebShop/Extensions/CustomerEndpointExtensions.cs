using Microsoft.AspNetCore.Mvc;
using WebShop.UnitOfWork;
using WebShopSolution.Sql.Entities;

namespace WebShop.Extensions;

public static class CustomerEndpointExtensions
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/customers").WithDisplayName("Customer Endpoints: Minimal Api");
        group.MapGet("", GetAllCustomers);
        group.MapGet("{id}", GetCustomerById);
        group.MapPost("", AddCustomer);
        group.MapPut("", UpdateCustomer);
        group.MapDelete("{id}", DeleteCustomer);

        return app;
    }

    #region Api methods
    public static async Task<IResult> GetAllCustomers([FromServices] IUnitOfWork unitOfWork)
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
    public static async Task<IResult> GetCustomerById([FromServices] IUnitOfWork unitOfWork, int id)
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
    public static async Task<IResult> AddCustomer([FromBody] Customer customer, [FromServices] IUnitOfWork unitOfWork)
    {
        if (customer is null)
            return Results.BadRequest();
        try
        {
            await unitOfWork.Customers.AddAsync(customer);
            unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
        return Results.Created($"/api/customers/{customer.Id}", customer);
    }
    public static async Task<IResult> UpdateCustomer([FromBody] Customer customer, [FromServices] IUnitOfWork unitOfWork)
    {

        if (customer is null)
            return Results.BadRequest();

        var existingOrder = await unitOfWork.Customers.GetByIdAsync(customer.Id);
        if (existingOrder is null)
            return Results.BadRequest();


        if (customer is null)
            return Results.BadRequest();
        try
        {
            await unitOfWork.Customers.UpdateCustomer(customer);
            unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
        return Results.Ok();
    }
    public static async Task<IResult> DeleteCustomer([FromServices] IUnitOfWork unitOfWork, int id)
    {
        if (id <= 0)
            return Results.BadRequest();
        var customer = await unitOfWork.Customers.GetByIdAsync(id);
        if (customer is null)
            return Results.BadRequest();

        try
        {
            await unitOfWork.Customers.DeleteAsync(id);
            unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
        return Results.Ok();
    }
    #endregion
}