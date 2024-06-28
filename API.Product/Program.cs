using API.Product.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Configure the HTTP request pipeline.

var connectionString = System.Environment.GetEnvironmentVariable("PRODUCT_DB");
var rabbitHost = System.Environment.GetEnvironmentVariable("RB_HOST");
var rabbitUser = System.Environment.GetEnvironmentVariable("RB_USER");
var rabbitPass = System.Environment.GetEnvironmentVariable("RB_PASS");

if (connectionString == null)
{
    builder.Services.AddDbContext<MS_ProductDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Product_DB")));
}
else
{
    builder.Services.AddDbContext<MS_ProductDB>(options => options.UseSqlServer(connectionString));
}


builder.Services.AddMassTransit(x =>
{
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
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
