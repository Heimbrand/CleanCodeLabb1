using Microsoft.EntityFrameworkCore;
using WebShop.Notifications;
using WebShop.UnitOfWork;
using WebShopSolution.Sql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Registrera Unit of Work i DI-container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<INotificationObserver, EmailNotification>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<WebShopDbContext>(options =>
    options.UseSqlServer(connectionString));






var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
