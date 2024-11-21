using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebShop.Extensions;
using WebShop.Notifications;
using WebShop.UnitOfWork;
using WebShopSolution.Sql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // Sparar f�r att beh�lla de inbyggda tj�nsterna
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<INotificationObserver, NotificationStrategyPattern>();
builder.Services.AddTransient<NotificationStrategy>();
builder.Services.AddSingleton<ProductSubject>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<WebShopDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; // L�gger till referenser f�r att undvika att serialisera samma objekt flera g�nger
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapProductEndpoints();
app.MapCustomerEndpoints();
app.MapOrderEndpoints();

var productSubject = app.Services.GetRequiredService<ProductSubject>();
var emailNotification = app.Services.GetRequiredService<INotificationObserver>();
productSubject.Attach(emailNotification);

app.Run();
