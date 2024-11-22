using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebShop.Extensions;
using WebShop.Notifications.Strategies;
using WebShop.UnitOfWork;
using WebShopSolution.Sql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // Sparar f�r att beh�lla de inbyggda tj�nsterna
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

//TODO: Du f�r kolla om din LocalHost connectionstring �r samma som min, annars f�r du �ndra i appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("LocalHostConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<WebShopDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; // F�r att undvika reference loop
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapProductEndpoints();
app.MapCustomerEndpoints();
app.MapOrderEndpoints();

// Eftersom mina registrerade som transient, s� tar den bara den sista. D�rav en foreach som tar in alla.
var productSubject = app.Services.GetRequiredService<ProductSubject>();
var notifications = app.Services.GetServices<INotificationObserver>();
foreach (var notification in notifications)
{
    productSubject.Attach(notification);
}

app.Run();
