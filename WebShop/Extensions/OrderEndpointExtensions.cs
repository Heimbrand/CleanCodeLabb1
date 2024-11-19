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
}