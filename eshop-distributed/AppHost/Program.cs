using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Create Postgres db as backing service.
var postgres = builder.AddPostgres("postgres").WithPgAdmin().WithDataVolume().WithLifetime(ContainerLifetime.Persistent);

// Create catalogdb using postgres.
var catalogdb = postgres.AddDatabase("catalogdb"); // Gives connectionInfo to the MS.

// Redis as backing service and using this in basket ms.
var cache = builder.AddRedis("cache").WithRedisInsight().WithDataVolume().WithLifetime(ContainerLifetime.Persistent);

var rabbitMq = builder.AddRabbitMQ("rabbitmq").WithManagementPlugin().WithDataVolume().WithLifetime(ContainerLifetime.Persistent);

var keycloak = builder.AddKeycloak("keycloak", 8080).WithDataVolume().WithLifetime(ContainerLifetime.Persistent);

// Projects and their references.
var catalog = builder.AddProject<Catalog>("catalog").WithReference(catalogdb).WithReference(rabbitMq)
    .WaitFor(catalogdb).WaitFor(rabbitMq);
var basket = builder.AddProject<Basket>("basket").WithReference(cache).WithReference(catalog).WithReference(rabbitMq)
    .WithReference(keycloak)
    .WaitFor(cache).WaitFor(rabbitMq).WaitFor(keycloak);

builder.Build().Run();
