using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Add projects and cloud native backing services.
builder.AddProject<Catalog>("catalog");

// Add projects and cloud native backing services.

builder.Build().Run();
