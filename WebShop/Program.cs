using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebShop.Extensions;
using WebShop.Notifications.Strategies;
using WebShop.UnitOfWork;
using WebShopSolution.Sql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // Sparar för att behålla de inbyggda tjänsterna
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<INotificationObserver, EmailNotification>();
builder.Services.AddTransient<INotificationObserver, SmsNotification>();
builder.Services.AddTransient<INotificationObserver, SmokeSignalNotification>();
builder.Services.AddTransient<INotificationObserver, HawkNotificationDeliveryServiceMACAAW>();
builder.Services.AddSingleton<ProductSubject>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.TagActionsBy(d =>
    {
        return new List<string>() { d.ActionDescriptor.DisplayName! };
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<WebShopDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; // Lägger till referenser för att undvika att serialisera samma objekt flera gånger
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapProductEndpoints();
app.MapCustomerEndpoints();
app.MapOrderEndpoints();

// Eftersom mina registrerade som transient, så tar den bara den sista. Därav en foreach som tar in alla.
var productSubject = app.Services.GetRequiredService<ProductSubject>();
var notifications = app.Services.GetServices<INotificationObserver>();
foreach (var notification in notifications)
{
    productSubject.Attach(notification);
}

app.Run();
