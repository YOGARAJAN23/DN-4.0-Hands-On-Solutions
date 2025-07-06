using RetailInventory.Data;
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
            var product2 = new Product { Name = "Mouse", Price = 800, Category = electronics };
            var product3 = new Product { Name = "Rice Bag", Price = 1500, Category = groceries };

            await context.Categories.AddRangeAsync(electronics, groceries);
            await context.Products.AddRangeAsync(product1, product2, product3);
            await context.SaveChangesAsync();

            Console.WriteLine("\nProducts with Price > 1000 (Sorted Descending):");
            var filtered = await context.Products
                .Where(p => p.Price > 1000)
                .OrderByDescending(p => p.Price)
                .ToListAsync();

            foreach (var p in filtered)
            {
                Console.WriteLine($"{p.Name} - ₹{p.Price}");
            }

            Console.WriteLine("\nProduct DTOs (Name & Price Only):");
            var productDTOs = await context.Products
                .Select(p => new { p.Name, p.Price })
                .ToListAsync();

            foreach (var dto in productDTOs)
            {
                Console.WriteLine($"{dto.Name} - ₹{dto.Price}");
            }
        }
    }