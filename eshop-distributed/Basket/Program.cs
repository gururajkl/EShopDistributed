var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

// Instruct basket ms to connect to the redis cache.
builder.AddRedisDistributedCache("cache");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapDefaultEndpoints();
app.UseHttpsRedirection();

app.Run();
