using API.Cart.Models;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using API.Cart.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = System.Environment.GetEnvironmentVariable("CART_DB");
var rabbitHost = System.Environment.GetEnvironmentVariable("RB_HOST");
var rabbitUser = System.Environment.GetEnvironmentVariable("RB_USER");
var rabbitPass = System.Environment.GetEnvironmentVariable("RB_PASS");

if (connectionString == null)
{
    builder.Services.AddDbContext<MS_CartDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Cart_DB")));
}
else
{
    builder.Services.AddDbContext<MS_CartDB>(options => options.UseSqlServer(connectionString));
}

builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<ProductUpdatedConsumer>();
    x.AddConsumersFromNamespaceContaining<ProductDeletedConsumer>();
    
    // Setup RabbitMQ Endpoint
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitHost, "/", host =>
        {
            host.Username(rabbitUser);
            host.Password(rabbitPass);
        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Carts}/{action=Index}/{id?}");

app.Run();
