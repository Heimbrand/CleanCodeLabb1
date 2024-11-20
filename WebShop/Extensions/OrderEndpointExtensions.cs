using Microsoft.AspNetCore.Mvc;
using WebShop.UnitOfWork;
using WebShopSolution.Sql.Entities;

namespace WebShop.Extensions;

public static class OrderEndpointExtensions
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/orders");
        group.MapGet("", GetAllOrders);
        group.MapGet("{id}", GetOrderById);
        group.MapPost("", AddOrder);
        group.MapPut("", UpdateOrder);
        group.MapDelete("{id}", DeleteOrder);

        return app;
    }

    #region Api methods
    public static async Task<IResult> GetAllOrders([FromServices] IUnitOfWork unitOfWork)
    {
        try
        {
            var orders = await unitOfWork.Orders.GetAllAsync();
            return orders is null ? Results.NotFound() : Results.Ok(orders);
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
    }
    public static async Task<IResult> GetOrderById([FromServices] IUnitOfWork unitOfWork, int id)
    {
        try
        {
            var order = await unitOfWork.Orders.GetByIdAsync(id);
            return order is null ? Results.NotFound($"Order with the id: {id} does not exist") : Results.Ok(order);
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
    }
    public static async Task<IResult> AddOrder([FromBody] Order order, [FromServices] IUnitOfWork unitOfWork)
    {
        if (order is null)
            return Results.BadRequest("Order is null");

        try
        {
            var existingCustomer = await unitOfWork.Customers.GetByIdAsync(order.CustomerId);

            if (existingCustomer is null)
                return Results.BadRequest("Customer does not exist");

            order.Customer = existingCustomer;

            if (order.OrderProducts is not null && order.OrderProducts.Any())
            {
                foreach (var orderProduct in order.OrderProducts)
                {
                    var existingProduct = await unitOfWork.Products.GetByIdAsync(orderProduct.ProductId);

                    if (existingProduct is null)
                        return Results.BadRequest("Product does not exist");

                    orderProduct.Product = existingProduct;
                }
            }

            order.ShippingDate = DateTime.UtcNow.Date;
            await unitOfWork.Orders.AddAsync(order);
            unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
        return Results.Ok(order);
    }
    public static async Task<IResult> UpdateOrder([FromBody] Order order, [FromServices] IUnitOfWork unitOfWork)
    {
        if (order is null)
            return Results.BadRequest("Order is null");
        try
        {
            await unitOfWork.Orders.UpdateOrder(order);
            unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
        return Results.Ok(order);
    }
    public static async Task<IResult> DeleteOrder([FromServices] IUnitOfWork unitOfWork, int id)
    {
        try
        { 
            await unitOfWork.Orders.DeleteAsync(id);
            unitOfWork.CommitAsync();
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }
    }
    #endregion
}