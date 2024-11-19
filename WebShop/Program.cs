using Microsoft.EntityFrameworkCore;
using WebShop.Extensions;
using WebShop.Notifications;
using WebShop.UnitOfWork;
using WebShopSolution.Sql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<INotificationObserver, EmailNotification>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<WebShopDbContext>(options =>
    options.UseSqlServer(connectionString));


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
#region Minimal api
app.MapProductEndpoints();
app.MapCustomerEndpoints();
app.MapOrderEndpoints();
#endregion
app.Run();
