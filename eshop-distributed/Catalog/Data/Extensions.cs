﻿namespace Catalog.Data;

public static class Extensions
{
    /// <summary>
    /// Migration extension method for the WebApplication.
    /// Current task is to ensure that the database is created and migrations are applied.
    /// </summary>
    /// <param name="app"></param>
    public static void UseMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
        context.Database.Migrate();
        DataSeeder.Seed(context);
    }
}

public class DataSeeder
{
    // Seeds the database with initial data if it is empty.
    public static void Seed(ProductDbContext context)
    {
        if (context.Products.Any()) return;

        context.Products.AddRange(Products);
        context.SaveChanges();
    }

    // Sample products for seeding the database.
    public static IEnumerable<Product> Products =>
    [
        new Product { Name = "Solar Powered Flashlight", Description = "A fantastic product for outdoor enthusiasts", Price = 19.99m, ImageUrl = "product1.png" },
        new Product { Name = "Hiking Poles", Description = "Ideal for camping and hiking trips", Price = 24.99m, ImageUrl = "product2.png" },
        new Product { Name = "Outdoor Rain Jacket", Description = "This product will keep you warm and dry in all weathers", Price = 49.99m, ImageUrl = "product3.png" },
        new Product { Name = "Survival Kit", Description = "A must-have for any outdoor adventurer", Price = 99.99m, ImageUrl = "product4.png" },
        new Product { Name = "Outdoor Backpack", Description = "This backpack is perfect for carrying all your outdoor essentials", Price = 39.99m, ImageUrl = "product5.png" },
        new Product { Name = "Camping Cookware", Description = "This cookware set is ideal for cooking outdoors", Price = 29.99m, ImageUrl = "product6.png" },
        new Product { Name = "Camping Stove", Description = "This stove is perfect for cooking outdoors", Price = 49.99m, ImageUrl = "product7.png" },
        new Product { Name = "Camping Lantern", Description = "This lantern is perfect for lighting up your campsite", Price = 19.99m, ImageUrl = "product8.png" },
        new Product { Name = "Camping Tent", Description = "This tent is perfect for camping trips", Price = 99.99m, ImageUrl = "product9.png" }
    ];
}
