using ConsoleApp1.Interfaces;
using ConsoleApp1.Services;

class Program
{
    static async Task Main()
    {
        ICRUDServices crudService = new CRUDServices();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 - Insert Product");
            Console.WriteLine("2 - Update Product");
            Console.WriteLine("3 - Delete Product");
            Console.WriteLine("4 - Get All Products");
            Console.WriteLine("5 - Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1":
                    await InsertProduct(crudService);
                    break;
                case "2":
                    await UpdateProduct(crudService);
                    break;
                case "3":
                    await DeleteProduct(crudService);
                    break;
                case "4":
                    await GetProducts(crudService);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private static async Task InsertProduct(ICRUDServices service)
    {
        Console.Write("Enter product name: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Enter product price: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            await service.InsertProductAsync(name, price);
            Console.WriteLine("Product inserted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid price.");
        }
    }

    private static async Task UpdateProduct(ICRUDServices service)
    {
        Console.Write("Enter product ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.Write("Enter new product name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Enter new product price: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                await service.UpdateProductAsync(id, name, price);
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid price.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private static async Task DeleteProduct(ICRUDServices service)
    {
        Console.Write("Enter product ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            await service.DeleteProductAsync(id);
            Console.WriteLine("Product deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private static async Task GetProducts(ICRUDServices service)
    {
        var products = await service.GetAllProductsAsync();
        if (products.Count != 0)
        {
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
        }
        }
        else
        {
            Console.WriteLine("Products data is empty");
        }
    }
}
