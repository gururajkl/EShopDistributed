var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapDefaultEndpoints();
app.UseHttpsRedirection();

app.Run();
