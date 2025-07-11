var builder = DistributedApplication.CreateBuilder(args);

// Add projects and cloud native backing services.

builder.Build().Run();
