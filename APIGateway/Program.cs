using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
builder.Services.AddOcelot(configuration).AddCacheManager(x =>
{
    x.WithDictionaryHandle();
}).AddPolly();

var app = builder.Build();

app.UseHttpsRedirection();

await app.UseOcelot();

app.MapControllers();

app.Run();
