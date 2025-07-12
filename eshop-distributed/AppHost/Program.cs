using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Create Postgres db as backing service.
var postgres = builder.AddPostgres("postgres").WithPgAdmin().WithDataVolume().WithLifetime(ContainerLifetime.Persistent);

// Create catalogdb using postgres.
var catalogdb = postgres.AddDatabase("catalogdb"); // Gives connectionInfo to the MS.

// Catalog project depends on catalogdb.
builder.AddProject<Catalog>("catalog").WithReference(catalogdb).WaitFor(catalogdb);

builder.Build().Run();
