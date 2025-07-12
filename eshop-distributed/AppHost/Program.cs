using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Create Postgres db as backing service.
var postgres = builder.AddPostgres("postgres").WithPgAdmin().WithDataVolume().WithLifetime(ContainerLifetime.Persistent);

// Create catalogdb using postgres.
var catalogdb = postgres.AddDatabase("catalogdb"); // Gives connectionInfo to the MS.

// Redis as backing service and using this in basket ms.
var cache = builder.AddRedis("cache").WithRedisInsight().WithDataVolume().WithLifetime(ContainerLifetime.Persistent);

// Projects and their references.
builder.AddProject<Catalog>("catalog").WithReference(catalogdb).WaitFor(catalogdb);
builder.AddProject<Basket>("basket").WithReference(cache).WaitFor(cache);

builder.Build().Run();
