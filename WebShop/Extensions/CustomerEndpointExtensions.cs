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
}