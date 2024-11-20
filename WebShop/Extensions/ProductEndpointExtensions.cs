using Microsoft.AspNetCore.Mvc;
using WebShop.UnitOfWork;
using WebShopSolution.Sql.Entities;

namespace WebShop.Extensions;

public static class ProductEndpointExtensions
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products");
        group.MapGet("", GetAllProducts);
        group.MapGet("{id}", GetProductById);
        group.MapPost("", AddProduct);
        group.MapPut("", UpdateProduct);
        group.MapDelete("{id}", DeleteProduct);

        return app;
    }

    #region Api methods
    public static async Task<IResult> GetAllProducts([FromServices] IUnitOfWork unitOfWork)
    {
        try
        {
            var products = await unitOfWork.Products.GetAllAsync();
            return products is null ? Results.NotFound() : Results.Ok(products);
        }
        catch (Exception e)
        {
            return Results.Problem($"Exception: {e.Message}");
        }

    }
    public static async Task<IResult> GetProductById([FromServices] IUnitOfWork unitOfWork, int id)
    {
        try
        {
            var product = await unitOfWork.Products.GetByIdAsync(id);
            return product is null ? Results.NotFound() : Results.Ok(product);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public static async Task<IResult> AddProduct([FromBody] Product product, [FromServices] IUnitOfWork unitOfWork)
    {
        if (product is null)
            return Results.BadRequest("Product is null");

        try
        {
            await unitOfWork.Products.AddAsync(product);
            unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            return Results.Problem($"Internal server error: {e.Message}");
        }

        return Results.Ok();
    }
    public static async Task<IResult> UpdateProduct([FromBody] Product product, [FromServices] IUnitOfWork unitOfWork)
    {
        if (product is null)
            return Results.BadRequest("Product is null");

        try
        {
            await unitOfWork.Products.UpdateProduct(product);
            unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            return Results.Problem($"Internal server error: {e.Message}");
        }

        return Results.Ok();
    }
    public static async Task<IResult> DeleteProduct([FromServices] IUnitOfWork unitOfWork, int id)
    {
        try
        {
            await unitOfWork.Products.DeleteAsync(id);
            unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            return Results.Problem($"Internal server error: {e.Message}");
        }

        return Results.Ok();
    }
    #endregion
}