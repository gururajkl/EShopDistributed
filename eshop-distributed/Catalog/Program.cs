var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

// Add postgreSql database connection.
// Register the ProductDbContext with Npgsql and the connection string.
builder.AddNpgsqlDbContext<ProductDbContext>("catalogdb");

// Add the ProductService to the service collection.
builder.Services.AddScoped<ProductService>();

// Register masstransit extension method here.
builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapDefaultEndpoints();

// Use the extension method to apply migrations and seed the database.
app.UseMigration();

// Extension class has all endpoints for product CRUD.
app.MapProductEndpoints();

app.UseHttpsRedirection();

app.Run();
