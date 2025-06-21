using System;
public class Product {
    public int ProdId { get; set; }
    public string ProdName { get; set; }
    public string Category { get; set; }
    public Product(int id, string name, string category) {
        ProdId = id;
        ProdName = name;
        Category = category;
    }
    public override string ToString() {
        return $"[{ProdId}] {ProdName} - {Category}";
    }
}
class Program {
    public static void Main(string[] args) {
        Product[] products = new Product[] {
            new Product(210, "Dell", "Electronics"),
            new Product(203, "Levi's", "Fashion"),
            new Product(215, "Sony", "Electronics"),
            new Product(204, "Puma", "Fashion"),
            new Product(207, "Backpack", "Accessories")
        };
        Console.Write("Please enter a Product ID to search: ");
        int targetId = int.Parse(Console.ReadLine());
        Console.WriteLine("Performing Linear Search");
        Product result1 = LinearSearchX(products, targetId);
        if (result1 != null) {
            Console.WriteLine("Product found: " + result1.ToString());
        } else {
            Console.WriteLine("Product not found via linear search.");
        }

        Array.Sort(products, (a, b) => a.ProdId.CompareTo(b.ProdId));
        Console.WriteLine("Performing Binary Search");
        Product result2 = BinarySearchX(products, targetId);
        if (result2 != null) {
            Console.WriteLine("Product found: " + result2.ToString());
        } else {
            Console.WriteLine("Product not found via binary search.");
        }
    }
    static Product LinearSearchX(Product[] products, int targetId) {
        foreach (var product in products) {
            if (product.ProdId == targetId) return product;
        }
        return null;
    }
    static Product BinarySearchX(Product[] products, int targetId) {
        int left = 0, right = products.Length - 1;
        while (left <= right) {
            int mid = (left + right) / 2;
            if (products[mid].ProdId == targetId) return products[mid];
            else if (products[mid].ProdId < targetId) left = mid + 1;
            else right = mid - 1;
        }
        return null;
    }
}