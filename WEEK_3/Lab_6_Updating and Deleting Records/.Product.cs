
﻿using RetailInventory.Data;
using RetailInventory.Models;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var context = new AppDbContext();

        context.Products.RemoveRange(context.Products);
        context.Categories.RemoveRange(context.Categories);
        await context.SaveChangesAsync();


        var electronics = new Category { Name = "Electronics" };
        var groceries = new Category { Name = "Groceries" };

        var product1 = new Product { Name = "Laptop", Price = 75000, Category = electronics };
        var product2 = new Product { Name = "Rice Bag", Price = 1200, Category = groceries };

        await context.Categories.AddRangeAsync(electronics, groceries);
        await context.Products.AddRangeAsync(product1, product2);
        await context.SaveChangesAsync();

        Console.WriteLine("Initial Data Added");

        var laptop = await context.Products.FirstOrDefaultAsync(p => p.Name == "Laptop");
        if (laptop != null)
        {
            laptop.Price = 70000;
            await context.SaveChangesAsync();
            Console.WriteLine("Laptop price updated to 70000");
        }

        var rice = await context.Products.FirstOrDefaultAsync(p => p.Name == "Rice Bag");
        if (rice != null)
        {
            context.Products.Remove(rice);
            await context.SaveChangesAsync();
            Console.WriteLine("'Rice Bag' product deleted");
        }


        var products = await context.Products.Include(p => p.Category).ToListAsync();
        Console.WriteLine("\nCurrent Products:");
        foreach (var p in products)
        {
            Console.WriteLine($"{p.Name} - ₹{p.Price} - Category: {p.Category.Name}");
        }
    }
}
