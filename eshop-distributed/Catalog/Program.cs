var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

// Add postgreSql database connection.
// Register the ProductDbContext with Npgsql and the connection string.
builder.AddNpgsqlDbContext<ProductDbContext>("catalogdb");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapDefaultEndpoints();

// Use the extension method to apply migrations and seed the database.
if (app.Environment.IsDevelopment())
    app.UseMigration();

app.Run();
