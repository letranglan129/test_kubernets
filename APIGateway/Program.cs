using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
builder.Services.AddOcelot(configuration);

var app = builder.Build();

app.UseHttpsRedirection();

await app.UseOcelot();

app.MapControllers();

app.Run();
