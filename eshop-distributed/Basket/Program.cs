var builder = WebApplication.CreateBuilder(args);

/* Add services to the container. */
builder.AddServiceDefaults();

// Instruct basket ms to connect to the redis cache.
builder.AddRedisDistributedCache("cache");
builder.Services.AddScoped<BasketService>();

var app = builder.Build();

/* Configure the HTTP request pipeline. */
app.MapDefaultEndpoints();

// Use extension method to expose basket service endpoints.
app.MapBasketEndpoints();

app.UseHttpsRedirection();

app.Run();
