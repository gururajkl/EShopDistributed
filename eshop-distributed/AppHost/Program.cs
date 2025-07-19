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

// Add Ollama as a backing service for AI capabilities.
var ollama = builder.AddOllama("ollama", 11434).WithDataVolume().WithLifetime(ContainerLifetime.Persistent).WithOpenWebUI();

var llama = ollama.AddModel("llama3.2");

// Projects and their references.
var catalog = builder.AddProject<Catalog>("catalog").WithReference(catalogdb).WithReference(rabbitMq)
    .WaitFor(catalogdb).WaitFor(rabbitMq);

var basket = builder.AddProject<Basket>("basket").WithReference(cache).WithReference(catalog).WithReference(rabbitMq)
    .WithReference(keycloak).WithReference(llama)
    .WaitFor(cache).WaitFor(rabbitMq).WaitFor(keycloak).WaitFor(llama);

builder.AddProject<WebApp>("webapp").WithExternalHttpEndpoints()
    .WithReference(catalog)
    .WaitFor(catalog);

builder.Build().Run();
